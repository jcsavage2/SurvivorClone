using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Timer : UIComponent
{
  private float time;
  private string text;
  private readonly Sprite timerBackground;

  public Timer(Vector2 _origin, float _verticalOffset, float _horizontalOffset)
    : base(_origin, _verticalOffset, _horizontalOffset)
  {
    time = 0;
    timerBackground = new Sprite(origin);
    timerBackground.LoadContent("UI/timer_background");
  }

  public void Update(GameTime gameTime)
  {
    base.Update();
    time += (float)gameTime.ElapsedGameTime.TotalSeconds;
    text = FormatText();
  }

  public string FormatText()
  {
    return TimeSpan.FromSeconds(time).ToString(@"mm\:ss\.ff");
  }

  public void Draw()
  {
    RenderManager.SpriteBatch.Draw(
      timerBackground.spriteTexture,
      drawPosition,
      timerBackground.rectangle,
      Color.White,
      0f,
      Vector2.Zero,
      Vector2.One,
      SpriteEffects.None,
      0f
    );
    RenderManager.SpriteBatch.DrawString(
      RenderManager.Font,
      text,
      new Vector2(drawPosition.X + (timerBackground.spriteTexture.Width / 3), drawPosition.Y + timerBackground.spriteTexture.Height / 5),
      Color.White
    );
  }
}
