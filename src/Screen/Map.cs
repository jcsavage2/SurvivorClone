using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Map
{
  public Point mapDimensionsPixels { get; set; }
  public Point mapDimensionsTiles { get; set; }
  private readonly Sprite[,] tiles;

  public Map(int _size)
  {
    tiles = new Sprite[_size, _size];
    mapDimensionsTiles = new Point(_size, _size);
  }

  public void LoadContent()
  {
    mapDimensionsPixels = new Point(mapDimensionsTiles.X * Globals.TILE_SIZE, mapDimensionsTiles.Y * Globals.TILE_SIZE);

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
        tiles[x, y] = new Sprite(new Vector2(x * Globals.TILE_SIZE, y * Globals.TILE_SIZE));
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
