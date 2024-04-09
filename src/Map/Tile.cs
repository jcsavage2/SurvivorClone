using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Tile : Sprite
{
  private readonly string texturePath;
  private readonly Map.TileType tileType;

  public Tile(Vector2 _startPosition, string _texturePath, Map.TileType _tileType)
    : base(_startPosition)
  {
    texturePath = _texturePath;
    tileType = _tileType;
  }

  public void LoadContent(RenderManager _renderManager)
  {
    base.LoadContent(_renderManager, "Maps/" + texturePath);
  }

  // --- GET --- //
  public Map.TileType GetTileType() => tileType;
}
