using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Map
{
  public Point MapDimensionsPixels { get; set; }
  public Point MapDimensionsTiles { get; set; }
  public int TileSize { get; set; }
  public Tile[] CollisionTiles { get; set; }
  private readonly Tile[,] tiles;

  public enum TileType
  {
    BASE,
    DECORATION,
    COLLISION
  }

  public Map(RenderManager _renderManager, int _mapSize, int _tileSize)
  {
    tiles = new Tile[_mapSize, _mapSize];
    MapDimensionsTiles = new Point(_mapSize, _mapSize);
    TileSize = _tileSize;

    MapDimensionsPixels = new Point(MapDimensionsTiles.X * TileSize, MapDimensionsTiles.Y * TileSize);

    createMap(_renderManager);
  }

  public void Draw(RenderManager _renderManager)
  {
    foreach (var tile in tiles)
    {
      tile.Draw(_renderManager);
    }
  }

  // --- HELPERS --- //

  // Create a map with a certain percentage of collision and decoration tiles
  private void createMap(RenderManager _renderManager)
  {
    string[] baseTilesFilePaths = new string[] { "grass_empty" },
      decorationTilesFilePaths = new string[] { "grass1", "grass2", "grass3", "grass4", "grass5", "grass6", "grass7", "grass8", "grass9" },
      collisionTilesFilePaths = new string[] { "bush1", "rock1" };

    // Allocate a certain percentage of the map to different file types
    int totalTiles = MapDimensionsTiles.X * MapDimensionsTiles.Y;
    int numCollisionTiles = (int)Math.Floor(totalTiles * .01);
    int numDecorationTiles = (int)Math.Floor(totalTiles * .5);

    bool[,] visited = new bool[MapDimensionsTiles.X, MapDimensionsTiles.Y];

    Random rand = new Random();
    CollisionTiles = new Tile[numCollisionTiles];
    for (int i = 0; i < numCollisionTiles; i++)
    {
      Tuple<int, int> tilePosition = getTilePosition(visited, rand);
      if (tilePosition.Item1 == -1)
      {
        continue;
      }
      tiles[tilePosition.Item1, tilePosition.Item2] = new Tile(
        _renderManager,
        "Maps/" + collisionTilesFilePaths[rand.Next(collisionTilesFilePaths.Length)],
        new Vector2(tilePosition.Item1 * TileSize, tilePosition.Item2 * TileSize),
        TileType.COLLISION
      );
      CollisionTiles[i] = tiles[tilePosition.Item1, tilePosition.Item2];
      visited[tilePosition.Item1, tilePosition.Item2] = true;
    }

    for (int i = 0; i < numDecorationTiles; i++)
    {
      Tuple<int, int> tilePosition = getTilePosition(visited, rand);
      if (tilePosition.Item1 == -1)
      {
        continue;
      }
      tiles[tilePosition.Item1, tilePosition.Item2] = new Tile(
        _renderManager,
        "Maps/" + decorationTilesFilePaths[rand.Next(decorationTilesFilePaths.Length)],
        new Vector2(tilePosition.Item1 * TileSize, tilePosition.Item2 * TileSize),
        TileType.DECORATION
      );
      visited[tilePosition.Item1, tilePosition.Item2] = true;
    }

    for (int x = 0; x < MapDimensionsTiles.X; x++)
    {
      for (int y = 0; y < MapDimensionsTiles.Y; y++)
      {
        if (!visited[x, y])
        {
          tiles[x, y] = new Tile(
            _renderManager,
            "Maps/" + baseTilesFilePaths[rand.Next(baseTilesFilePaths.Length)],
            new Vector2(x * TileSize, y * TileSize),
            TileType.BASE
          );
          visited[x, y] = true;
        }
      }
    }
  }

  // Fetch a random tile position, handle collisions if a tile has already been placed
  private Tuple<int, int> getTilePosition(bool[,] _visited, Random _rand, int retryCount = 0)
  {
    if (retryCount == 3)
    {
      return new Tuple<int, int>(-1, -1);
    }

    int x = _rand.Next(MapDimensionsTiles.X),
      y = _rand.Next(MapDimensionsTiles.Y);

    if (!_visited[x, y])
    {
      return new Tuple<int, int>(x, y);
    }

    return getTilePosition(_visited, _rand, retryCount + 1);
  }
}
