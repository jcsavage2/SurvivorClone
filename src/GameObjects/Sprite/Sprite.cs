using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Sprite
{
  protected Texture2D spriteTexture { get; set; }
  protected Rectangle drawRectangle
  {
    get
    {
      return new Rectangle(Vector2.Zero, Shape.Size);
    }
  }

  public Shape Shape { get; set; }

  public Sprite(RenderManager _renderManager, string _texturePath, Vector2 _position, Geometry.CollisionTypes _collisionType = Geometry.CollisionTypes.RECTANGLE)
  {
    spriteTexture = _renderManager.Content.Load<Texture2D>(_texturePath);
    Point _size = new Point(spriteTexture.Width, spriteTexture.Height);
    Shape = Shape.CreateShape(_position, _size, _collisionType);
  }

  public virtual void Draw(RenderManager _renderManager)
  {
    _renderManager.DrawTexture(spriteTexture, Shape.Position, drawRectangle);
  }

  public bool Intersects(Sprite _sprite)
  {
    return Shape.IntersectsWith(_sprite.Shape);
  }

  public bool Intersects(Rectangle _rectangle)
  {
    return Shape.IntersectsWith(_rectangle);
  }

  public bool Intersects(Circle _circle)
  {
    return Shape.IntersectsWith(_circle);
  }
}
