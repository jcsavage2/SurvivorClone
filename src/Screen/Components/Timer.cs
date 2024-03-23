using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Timer : Sprite
{
  private float time;
  private string text;
  private readonly SpriteFont font;

  public Timer(Vector2 startPosition, SpriteFont font)
    : base(startPosition)
  {
    this.font = font;
  }

  public void Update(GameTime gameTime)
  {
    time += (float)gameTime.ElapsedGameTime.TotalSeconds;
    text = FormatText();
  }

  public string FormatText()
  {
    return TimeSpan.FromSeconds(time).ToString(@"mm\:ss\.ff");
  }

  public override void Draw()
  {
    base.Draw();
    Globals.SpriteBatch.DrawString(font, text, new Vector2(position.X - 30, position.Y - 8), Color.White);
  }
}
