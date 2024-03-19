using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Sprite
{
  public Texture2D spriteTexture { get; set; }

  // State
  public Vector2 position { get; set; }
  public Vector2 center { get; set; }
  public Vector2 minPos { get; set; }
  public Vector2 maxPos { get; set; }

  public Sprite(Vector2 startPosition)
  {
    position = startPosition;
  }

  public void SetBounds(Point mapSizePixels)
  {
    minPos = new Vector2(5, 5);
    maxPos = new Vector2(mapSizePixels.X - spriteTexture.Width + 5, mapSizePixels.Y - spriteTexture.Height + 5);
  }

  public virtual void LoadContent(string texturePath)
  {
    spriteTexture = Globals.Content.Load<Texture2D>("Sprites/" + texturePath);
    center = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
  }

  public virtual void Draw()
  {
    Globals.SpriteBatch.Draw(spriteTexture, position, null, Color.White, 0f, center, Vector2.One, SpriteEffects.None, 0f);
  }
}
