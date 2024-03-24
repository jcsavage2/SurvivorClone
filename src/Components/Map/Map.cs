using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Map
{
  public Point mapDimensionsPixels { get; set; }
  public Point mapDimensionsTiles { get; set; }
  public int tileSize { get; set; }
  private readonly Sprite[,] tiles;

  public Map(int _mapSize, int _tileSize)
  {
    tiles = new Sprite[_mapSize, _mapSize];
    mapDimensionsTiles = new Point(_mapSize, _mapSize);
    tileSize = _tileSize;
  }

  public void LoadContent()
  {
    mapDimensionsPixels = new Point(mapDimensionsTiles.X * tileSize, mapDimensionsTiles.Y * tileSize);

    Random rand = new Random();
    for (int x = 0; x < mapDimensionsTiles.X; x++)
    {
      for (int y = 0; y < mapDimensionsTiles.Y; y++)
      {
        string val = (rand.Next(6) + 1).ToString();
        if (x % 3 == 0)
        {
          val = "1";
        }
        tiles[x, y] = new Sprite(new Vector2(x * tileSize, y * tileSize));
        tiles[x, y].LoadContent("Sprites/grass_tile" + val);
      }
    }
  }

  public void Draw()
  {
    foreach (var tile in tiles)
    {
      tile.Draw();
    }
  }
}
