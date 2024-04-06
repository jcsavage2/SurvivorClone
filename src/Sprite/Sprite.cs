using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Sprite
{
  private Texture2D spriteTexture { get; set; }
  private Rectangle rectangle { get; set; }

  // State
  private Vector2 position { get; set; }
  private Vector2 center { get; set; }
  private Vector2 minPos { get; set; }
  private Vector2 maxPos { get; set; }

  public Sprite(RenderManager _renderManager, Vector2 startPosition)
  {
    position = startPosition;
  }

  public virtual void LoadContent(RenderManager _renderManager, string _texturePath)
  {
    spriteTexture = _renderManager.GetContent().Load<Texture2D>(_texturePath);
    center = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
    rectangle = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
  }

  // Update the dimensions of the rectangle used to render the sprite
  public void UpdateDimensions(Vector2 _size)
  {
    rectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
  }

  public virtual void Draw(RenderManager _renderManager)
  {
    _renderManager.GetSpriteBatch().Draw(spriteTexture, position, rectangle, Color.White, 0f, center, Vector2.One, SpriteEffects.None, 0f);
  }

  // Setters
  public void SetPosition(Vector2 _position) => position = _position;

  public void SetBounds(Vector2 _minPos, Vector2 _maxPos)
  {
    minPos = _minPos;
    maxPos = _maxPos;
  }

  public void SetRectangle(Rectangle _rectangle) => rectangle = _rectangle;

  public void SetTexture(Texture2D _texture) => spriteTexture = _texture;

  public void SetCenter(Vector2 _center) => center = _center;

  // Getters
  public Vector2 GetPosition() => position;

  public Vector2 GetCenter() => center;

  public Rectangle GetRectangle() => rectangle;

  public Vector2 GetMinPos() => minPos;

  public Vector2 GetMaxPos() => maxPos;

  public Texture2D GetTexture() => spriteTexture;

  public virtual Vector2 GetSize() => new Vector2(rectangle.Width, rectangle.Height);

  internal void LoadContent(RenderManager renderManager)
  {
    throw new NotImplementedException();
  }
}
