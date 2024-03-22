using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class ProgressBar
{
  private readonly Vector2 origin;
  private readonly SpriteFont font;
  private float progress;
  private readonly RectangleSprite backgroundRectangle;
  private readonly RectangleSprite fillRectangle;

  public ProgressBar(SpriteFont _font, Color _backgroundColor, Color _fillColor, Vector2 _origin, Vector2 size, float _progress = 1)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);
    font = _font;
    origin = _origin;

    _backgroundColor = Color.Lerp(_backgroundColor, Color.Black, 0.1f);
    _fillColor = Color.Lerp(_fillColor, Color.Black, 0.1f);
    backgroundRectangle = new RectangleSprite(origin, size, _backgroundColor, Color.Black);
    fillRectangle = new RectangleSprite(origin, new Vector2(size.X * progress, size.Y), _fillColor, Color.Black);
    backgroundRectangle.LoadContent();
    fillRectangle.LoadContent();
  }

  public void UpdateProgress(float _progress)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);
    fillRectangle.UpdateDimensions(new Vector2(backgroundRectangle.rectangle.Width * progress, backgroundRectangle.rectangle.Height));
  }

  public void Draw()
  {
    int displayPercentage = (int)(progress * 100);
    backgroundRectangle.Draw();
    fillRectangle.Draw();
    Globals.SpriteBatch.DrawString(font, displayPercentage + " %", new Vector2(origin.X + 8, origin.Y + 2), Color.White);
  }
}
