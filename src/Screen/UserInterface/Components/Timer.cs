using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Timer : UIComponent
{
  private float time;
  private string text;
  private readonly Sprite timerBackground;

  private Vector2 textDrawPos;

  public Timer(RenderManager _renderManager, Vector2 _origin, int _verticalOffset, int _horizontalOffset)
    : base(_renderManager, _origin, _verticalOffset, _horizontalOffset)
  {
    time = 0;
    timerBackground = new Sprite(_renderManager, "UI/timer_background", _origin);
    text = FormatText();
  }

  public void Update(RenderManager _renderManager, GameTime gameTime)
  {
    time += (float)gameTime.ElapsedGameTime.TotalSeconds;
    text = FormatText();

    textDrawPos = GetTextCenterDrawPosition(_renderManager, text, timerBackground);
  }

  public string FormatText()
  {
    return TimeSpan.FromSeconds(time).ToString(@"mm\:ss\.ff");
  }

  public void Draw(RenderManager _renderManager)
  {
    timerBackground.Draw(_renderManager, position);
    _renderManager.DrawString(text, textDrawPos, Color.White);
  }
}
