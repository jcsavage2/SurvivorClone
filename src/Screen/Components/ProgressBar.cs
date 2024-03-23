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
  private readonly Sprite background;
  private readonly Sprite fill;

  public ProgressBar(SpriteFont _font, Vector2 _origin, float _progress = 1)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);
    font = _font;
    origin = _origin;

    background = new Sprite(origin);
    fill = new Sprite(origin);
    background.LoadContent("UI/back");
    fill.LoadContent("UI/front");
  }

  public void UpdateProgress(float _progress)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);
    fill.UpdateDimensions(new Vector2(background.rectangle.Width * progress, background.rectangle.Height));
  }

  public void Draw()
  {
    int displayPercentage = (int)(progress * 100);
    background.Draw();
    fill.Draw();
    Globals.SpriteBatch.DrawString(font, displayPercentage + " %", new Vector2(origin.X - 20, origin.Y - 8), Color.White);
  }
}
