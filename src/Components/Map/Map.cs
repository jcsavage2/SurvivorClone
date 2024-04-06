using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Map
{
  private Point mapDimensionsPixels { get; set; }
  private Point mapDimensionsTiles { get; set; }
  private int tileSize { get; set; }
  private readonly Tile[,] tiles;

  // Generation resources
  private readonly string[] baseTilesFilePaths;
  private readonly string[] collisionTilesFilePaths;
  private readonly string[] decorationTilesFilePaths;

  public enum TileType
  {
    BASE,
    DECORATION,
    COLLISION
  }

  public Map(int _mapSize, int _tileSize)
  {
    tiles = new Tile[_mapSize, _mapSize];
    mapDimensionsTiles = new Point(_mapSize, _mapSize);
    tileSize = _tileSize;

    mapDimensionsPixels = new Point(mapDimensionsTiles.X * tileSize, mapDimensionsTiles.Y * tileSize);

    baseTilesFilePaths = new string[] { "grass_empty" };
    decorationTilesFilePaths = new string[] { "grass1", "grass2", "grass3", "grass4", "grass5", "grass6", "grass7", "grass8", "grass9" };
    collisionTilesFilePaths = new string[] { "bush1", "rock1" };
  }

  public void LoadContent(RenderManager _renderManager)
  {
    // Allocate a certain percentage of the map to different file types
    int totalTiles = mapDimensionsTiles.X * mapDimensionsTiles.Y;
    int numCollisionTiles = (int)Math.Floor(totalTiles * .03);
    int numDecorationTiles = (int)Math.Floor(totalTiles * .5);

    Random rand = new Random();
    for (int i = 0; i < numCollisionTiles; i++)
    {
      Tuple<int, int> tilePosition = getTilePosition(rand);
      tiles[tilePosition.Item1, tilePosition.Item2] = new Tile(
        _renderManager,
        new Vector2(tilePosition.Item1 * tileSize, tilePosition.Item2 * tileSize),
        collisionTilesFilePaths[rand.Next(collisionTilesFilePaths.Length)],
        TileType.COLLISION
      );
    }

    for (int i = 0; i < numDecorationTiles; i++)
    {
      Tuple<int, int> tilePosition = getTilePosition(rand);
      tiles[tilePosition.Item1, tilePosition.Item2] = new Tile(
        _renderManager,
        new Vector2(tilePosition.Item1 * tileSize, tilePosition.Item2 * tileSize),
        decorationTilesFilePaths[rand.Next(decorationTilesFilePaths.Length)],
        TileType.DECORATION
      );
    }

    for (int x = 0; x < mapDimensionsTiles.X; x++)
    {
      for (int y = 0; y < mapDimensionsTiles.Y; y++)
      {
        if (tiles[x, y] == null)
        {
          tiles[x, y] = new Tile(
            _renderManager,
            new Vector2(x * tileSize, y * tileSize),
            baseTilesFilePaths[rand.Next(baseTilesFilePaths.Length)],
            TileType.BASE
          );
        }

        tiles[x, y].LoadContent(_renderManager);
      }
    }
  }

  public void Draw(RenderManager _renderManager)
  {
    foreach (var tile in tiles)
    {
      tile.Draw(_renderManager);
    }
  }

  // Private helpers

  // Fetch a random tile position, handle collisions if a tile has already been placed
  private Tuple<int, int> getTilePosition(Random _rand)
  {
    int x = _rand.Next(mapDimensionsTiles.X),
      y = _rand.Next(mapDimensionsTiles.Y);

    Tuple<int, int> tilePos = handleTileCollision(_rand, x, y);
    if (tilePos.Item1 == -1)
    {
      GameManager.Logger.ThrowErrorLog("Unable to find tile position: " + tilePos.Item1 + ", " + tilePos.Item2);
    }
    return tilePos;
  }

  private Tuple<int, int> handleTileCollision(Random _rand, int _x, int _y)
  {
    if (tiles[_x, _y] == null)
    {
      return new Tuple<int, int>(_x, _y);
    }

    Tuple<int, int> pos = new Tuple<int, int>(-1, -1);
    if (_x < mapDimensionsTiles.X - 1)
    {
      pos = handleTileCollision(_rand, _x + 1, _y);
      if (pos.Item1 != -1)
      {
        return pos;
      }
    }

    if (_x > 1)
    {
      pos = handleTileCollision(_rand, _x - 1, _y);
      if (pos.Item1 != -1)
      {
        return pos;
      }
    }

    if (_y < mapDimensionsTiles.Y - 1)
    {
      pos = handleTileCollision(_rand, _x, _y + 1);
      if (pos.Item1 != -1)
      {
        return pos;
      }
    }

    if (_y > 1)
    {
      pos = handleTileCollision(_rand, _x, _y - 1);
      if (pos.Item1 != -1)
      {
        return pos;
      }
    }

    return pos;
  }

  // Getters
  public Point GetMapDimensionsPixels() => mapDimensionsPixels;

  public Point GetMapDimensionsTiles() => mapDimensionsTiles;

  public int GetTileSize() => tileSize;
}
