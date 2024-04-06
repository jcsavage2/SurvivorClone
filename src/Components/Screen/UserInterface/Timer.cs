using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class Timer : UIComponent
{
  private float time;
  private string text;
  private readonly Sprite timerBackground;

  public Timer(RenderManager _renderManager, Vector2 _origin, float _verticalOffset, float _horizontalOffset)
    : base(_renderManager, _origin, _verticalOffset, _horizontalOffset)
  {
    time = 0;
    timerBackground = new Sprite(_renderManager, _origin);
    timerBackground.LoadContent(_renderManager, "UI/timer_background");
  }

  public void Update(RenderManager _renderManager, GameTime gameTime)
  {
    time += (float)gameTime.ElapsedGameTime.TotalSeconds;
    text = FormatText();
  }

  public string FormatText()
  {
    return TimeSpan.FromSeconds(time).ToString(@"mm\:ss\.ff");
  }

  public void Draw(RenderManager _renderManager)
  {
    _renderManager
      .GetSpriteBatch()
      .Draw(
        timerBackground.GetTexture(),
        drawPosition,
        timerBackground.GetRectangle(),
        Color.White,
        0f,
        Vector2.Zero,
        Vector2.One,
        SpriteEffects.None,
        0f
      );
    _renderManager
      .GetSpriteBatch()
      .DrawString(
        _renderManager.GetFont(),
        text,
        new Vector2(drawPosition.X + (timerBackground.GetTexture().Width / 3), drawPosition.Y + timerBackground.GetTexture().Height / 5),
        Color.White
      );
  }
}
