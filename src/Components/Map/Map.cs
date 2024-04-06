using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Map
{
  private Point mapDimensionsPixels { get; set; }
  private Point mapDimensionsTiles { get; set; }
  private int tileSize { get; set; }
  private readonly Tile[,] tiles;

  private Tile[] collisionTiles;

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
  }

  public void LoadContent(RenderManager _renderManager)
  {
    string[] baseTilesFilePaths = new string[] { "grass_empty" },
      decorationTilesFilePaths = new string[] { "grass1", "grass2", "grass3", "grass4", "grass5", "grass6", "grass7", "grass8", "grass9" },
      collisionTilesFilePaths = new string[] { "bush1", "rock1" };

    // Allocate a certain percentage of the map to different file types
    int totalTiles = mapDimensionsTiles.X * mapDimensionsTiles.Y;
    int numCollisionTiles = (int)Math.Floor(totalTiles * .02);
    int numDecorationTiles = (int)Math.Floor(totalTiles * .5);

    Random rand = new Random();
    collisionTiles = new Tile[numCollisionTiles];
    for (int i = 0; i < numCollisionTiles; i++)
    {
      Tuple<int, int> tilePosition = getTilePosition(rand, 0);
      if (tilePosition.Item1 == -1)
      {
        continue;
      }
      tiles[tilePosition.Item1, tilePosition.Item2] = new Tile(
        new Vector2(tilePosition.Item1 * tileSize, tilePosition.Item2 * tileSize),
        collisionTilesFilePaths[rand.Next(collisionTilesFilePaths.Length)],
        TileType.COLLISION
      );
      collisionTiles[i] = tiles[tilePosition.Item1, tilePosition.Item2];
    }

    for (int i = 0; i < numDecorationTiles; i++)
    {
      Tuple<int, int> tilePosition = getTilePosition(rand, 0);
      if (tilePosition.Item1 == -1)
      {
        continue;
      }
      tiles[tilePosition.Item1, tilePosition.Item2] = new Tile(
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
          tiles[x, y] = new Tile(new Vector2(x * tileSize, y * tileSize), baseTilesFilePaths[rand.Next(baseTilesFilePaths.Length)], TileType.BASE);
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
  private Tuple<int, int> getTilePosition(Random _rand, int retryCount)
  {
    if (retryCount == 3)
    {
      return new Tuple<int, int>(-1, -1);
    }

    int x = _rand.Next(mapDimensionsTiles.X),
      y = _rand.Next(mapDimensionsTiles.Y);

    if (tiles[x, y] == null)
    {
      return new Tuple<int, int>(x, y);
    }

    return getTilePosition(_rand, retryCount + 1);
  }

  // Getters
  public Tile[] GetCollisionTiles() => collisionTiles;

  public Point GetMapDimensionsPixels() => mapDimensionsPixels;

  public Point GetMapDimensionsTiles() => mapDimensionsTiles;

  public int GetTileSize() => tileSize;
}
