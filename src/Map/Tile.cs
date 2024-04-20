using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Tile : Sprite
{
  private readonly Map.TileType tileType;

  public Tile(RenderManager _renderManager, string _texturePath, Vector2 _startPosition, Map.TileType _tileType)
    : base(_renderManager, _texturePath, _startPosition)
  {
    tileType = _tileType;
  }

  // --- GET --- //
  public Map.TileType GetTileType() => tileType;
}
