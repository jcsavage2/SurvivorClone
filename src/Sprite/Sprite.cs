using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Sprite
{
  public Texture2D spriteTexture { get; set; }
  public Rectangle rectangle { get; set; }

  // State
  public Vector2 position { get; set; }
  public Vector2 center { get; set; }
  public Vector2 minPos { get; set; }
  public Vector2 maxPos { get; set; }

  public Sprite(Vector2 startPosition)
  {
    position = startPosition;
  }

  public virtual void LoadContent(string texturePath)
  {
    spriteTexture = Globals.Content.Load<Texture2D>(texturePath);
    center = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
    rectangle = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
  }

  public void SetBounds(Point mapSizePixels)
  {
    minPos = new Vector2(5, 5);
    maxPos = new Vector2(mapSizePixels.X - spriteTexture.Width + 5, mapSizePixels.Y - spriteTexture.Height + 5);
  }

  public void UpdateDimensions(Vector2 _size)
  {
    rectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
  }

  public virtual void Draw()
  {
    Globals.SpriteBatch.Draw(spriteTexture, position, rectangle, Color.White, 0f, center, Vector2.One, SpriteEffects.None, 0f);
  }
}
