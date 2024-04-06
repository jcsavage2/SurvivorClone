using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Tile : Sprite
{
  private readonly string texturePath;
  private readonly Map.TileType tileType;

  public Tile(RenderManager _renderManager, Vector2 _startPosition, string _texturePath, Map.TileType _tileType)
    : base(_renderManager, _startPosition)
  {
    texturePath = _texturePath;
    tileType = _tileType;
  }

  public new void LoadContent(RenderManager _renderManager)
  {
    SetTexture(_renderManager.GetContent().Load<Texture2D>("Maps/" + texturePath));
    SetCenter(new Vector2(GetTexture().Width / 2, GetTexture().Height / 2));
    SetRectangle(new Rectangle(0, 0, GetTexture().Width, GetTexture().Height));
  }

  // Getters
  public Map.TileType GetTileType() => tileType;
}
