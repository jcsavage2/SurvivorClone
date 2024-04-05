using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Map
{
  private Point mapDimensionsPixels { get; set; }
  private Point mapDimensionsTiles { get; set; }
  private int tileSize { get; set; }
  private readonly Sprite[,] tiles;

  // Generation resources
  private readonly string[] baseTiles;
  private readonly string[] collisionTiles;
  private readonly string[] decorationTiles;

  public Map(int _mapSize, int _tileSize)
  {
    tiles = new Sprite[_mapSize, _mapSize];
    mapDimensionsTiles = new Point(_mapSize, _mapSize);
    tileSize = _tileSize;

    mapDimensionsPixels = new Point(mapDimensionsTiles.X * tileSize, mapDimensionsTiles.Y * tileSize);

    baseTiles = new string[] { "grass_empty" };
    decorationTiles = new string[] { "grass1", "grass2", "grass3", "grass4", "grass5", "grass6", "grass7", "grass8", "grass9" };
    collisionTiles = new string[] { "bush1", "rock1" };
  }

  public void LoadContent(RenderManager _renderManager)
  {
    Random rand = new Random();
    for (int x = 0; x < mapDimensionsTiles.X; x++)
    {
      for (int y = 0; y < mapDimensionsTiles.Y; y++)
      {
        string texturePath = "Maps/";
        if (x % 4 == 0)
        {
          texturePath += decorationTiles[rand.Next(decorationTiles.Length)];
        }
        else if (x % 20 == 0)
        {
          texturePath += collisionTiles[rand.Next(collisionTiles.Length)];
        }
        else
        {
          texturePath += baseTiles[0];
        }

        tiles[x, y] = new Sprite(_renderManager, new Vector2(x * tileSize, y * tileSize));
        tiles[x, y].LoadContent(_renderManager, texturePath);
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

  // Getters
  public Point GetMapDimensionsPixels() => mapDimensionsPixels;

  public Point GetMapDimensionsTiles() => mapDimensionsTiles;

  public int GetTileSize() => tileSize;
}
