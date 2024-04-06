using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

  public new void LoadContent(RenderManager _renderManager)
  {
    spriteTexture = _renderManager.GetContent().Load<Texture2D>("Maps/" + texturePath);
    center = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
    rectangle = new Rectangle((int)position.X, (int)position.Y, spriteTexture.Width, spriteTexture.Height);
    drawRectangle = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
  }

  // Getters
  public Map.TileType GetTileType() => tileType;
}
