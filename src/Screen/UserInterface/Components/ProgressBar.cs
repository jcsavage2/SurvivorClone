using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class ProgressBar : UIComponent
{
  private float progress;
  private readonly Sprite background;
  private readonly Sprite fill;

  private Vector2 textDrawPos;
  private string text;

  public ProgressBar(RenderManager _renderManager, Vector2 _origin, int _verticalOffset, int _horizontalOffset, float _progress = 1)
    : base(_renderManager, _origin, _verticalOffset, _horizontalOffset)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);

    background = new Sprite(_origin);
    fill = new Sprite(_origin);
    background.LoadContent(_renderManager, "UI/back");
    fill.LoadContent(_renderManager, "UI/front");
    text = getTimerText();
  }

  public void Update(RenderManager _renderManager, float _progress)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);
    int newWidth = (int)(background.GetSize().X * .5);
    fill.SetSize(new Point(newWidth, background.GetSize().Y));
    text = getTimerText();
    textDrawPos = GetTextCenterDrawPosition(_renderManager, text, background);
  }

  public void Draw(RenderManager _renderManager)
  {
    background.Draw(_renderManager, position);
    fill.Draw(_renderManager, position);
    _renderManager.DrawString(text, textDrawPos, Color.White);
  }

  // --- HELPERS --- //

  private string getTimerText()
  {
    return (int)(progress * 100) + " %";
  }
}
