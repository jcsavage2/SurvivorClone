using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Sprite
{
  public Texture2D spriteTexture { get; set; }
  public Rectangle rectangle { get; set; }

  // State
  public Vector2 position { get; set; }
  protected Vector2 drawPosition
  {
    get
    {
      Vector2 divisor = RenderManager.WindowSize.ToVector2() / RenderManager.RenderSize.ToVector2();
      return position / divisor;
    }
  }
  public Vector2 center { get; set; }
  public Vector2 minPos { get; set; }
  public Vector2 maxPos { get; set; }

  // Constants
  public const int ORIGIN_OFFSET = 5;

  public Sprite(Vector2 startPosition)
  {
    position = startPosition;
  }

  public virtual void LoadContent(string texturePath)
  {
    spriteTexture = RenderManager.Content.Load<Texture2D>(texturePath);
    center = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
    rectangle = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
  }

  public virtual void SetBounds(Point mapSizePixels)
  {
    minPos = new Vector2(ORIGIN_OFFSET, ORIGIN_OFFSET);
    maxPos = new Vector2(mapSizePixels.X - spriteTexture.Width, mapSizePixels.Y - spriteTexture.Height);
  }

  // Update the dimensions of the rectangle used to render the sprite
  public void UpdateDimensions(Vector2 _size)
  {
    rectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
  }

  public virtual void Draw()
  {
    RenderManager.SpriteBatch.Draw(spriteTexture, drawPosition, rectangle, Color.White, 0f, center, Vector2.One, SpriteEffects.None, 0f);
  }
}
