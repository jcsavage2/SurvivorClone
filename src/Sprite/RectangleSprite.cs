using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class RectangleSprite : Sprite
{
  public Rectangle rectangle { get; set; }
  private Color rectangleColor;
  private Color borderColor;

  public RectangleSprite(Vector2 position, Vector2 _size, Color _rectangleColor, Color _borderColor)
    : base(position)
  {
    rectangleColor = _rectangleColor;
    borderColor = _borderColor;
    rectangle = new Rectangle((int)position.X, (int)position.Y, (int)_size.X, (int)_size.Y);
  }

  public void UpdateDimensions(Vector2 _size)
  {
    rectangle = new Rectangle((int)position.X, (int)position.Y, (int)_size.X, (int)_size.Y);
  }

  public void LoadContent()
  {
    spriteTexture = new Texture2D(Globals.Graphics.GraphicsDevice, 1, 1);
    spriteTexture.SetData(new[] { rectangleColor });
  }

  //Draw the rectangles border using 1 pixel wide lines
  private void DrawBorder()
  {
    Globals.SpriteBatch.Draw(spriteTexture, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), borderColor);
    Globals.SpriteBatch.Draw(spriteTexture, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height), borderColor);
    Globals.SpriteBatch.Draw(spriteTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), borderColor);
    Globals.SpriteBatch.Draw(spriteTexture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1), borderColor);
  }

  public override void Draw()
  {
    Globals.SpriteBatch.Draw(spriteTexture, rectangle, Color.White);
    DrawBorder();
  }
}
