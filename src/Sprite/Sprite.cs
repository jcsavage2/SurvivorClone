using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Sprite
{
  protected Texture2D spriteTexture { get; set; }
  protected Point size { get; set; }
  protected Vector2 position { get; set; }

  public Sprite(Vector2 startPosition)
  {
    position = startPosition;
  }

  public virtual void LoadContent(RenderManager _renderManager, string _texturePath)
  {
    spriteTexture = _renderManager.GetContent().Load<Texture2D>(_texturePath);
    size = new Point(spriteTexture.Width, spriteTexture.Height);
  }

  public virtual void Draw(RenderManager _renderManager)
  {
    _renderManager.DrawTexture(spriteTexture, position, new Rectangle(0, 0, size.X, size.Y));
  }

  public virtual void Draw(RenderManager _renderManager, Vector2 _position)
  {
    _renderManager.DrawTexture(spriteTexture, _position, new Rectangle(0, 0, size.X, size.Y));
  }

  // --- SET --- //
  public void SetSize(Point _size) => size = _size;

  public void SetPosition(Vector2 _position) => position = _position;

  // --- GET --- //
  public Rectangle GetBoundingBox() => new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);

  public Rectangle GetBoundingBox(Vector2 _position) => new Rectangle((int)_position.X, (int)_position.Y, size.X, size.Y);

  public Vector2 GetCenter() => new Vector2(position.X + size.X / 2, position.Y + size.Y / 2);

  public Point GetSize() => size;

  public Vector2 GetPosition() => position;

  public Vector2 GetNewPosition(Vector2 velocity) => position + velocity;
}
