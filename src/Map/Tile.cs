using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Tile : Sprite
{
  public Map.TileType TileType { get; set; }

  public Tile(RenderManager _renderManager, string _texturePath, Vector2 _startPosition, Map.TileType _tileType)
    : base(_renderManager, _texturePath, _startPosition)
  {
    TileType = _tileType;
  }

}
