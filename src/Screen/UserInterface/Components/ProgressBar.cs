using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class ProgressBar : UIComponent
{
  public Point Size
  {
    get
    {
      return background.Shape.Size;
    }
  }
  private float progress;
  private readonly Sprite background;
  private readonly Sprite fill;
  private Vector2 textDrawPos;
  private string text;

  public ProgressBar(RenderManager _renderManager, Vector2 _position, int _verticalOffset, int _horizontalOffset, float _progress = 1)
    : base(_renderManager, _position, _verticalOffset, _horizontalOffset)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);

    background = new Sprite(_renderManager, "UI/healthbar_background", _position);
    fill = new Sprite(_renderManager, "UI/healthbar_front", _position);
    text = getTimerText();
  }

  public void Update(RenderManager _renderManager, float _progress)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);
    int newWidth = (int)(background.Shape.Width * progress);
    fill.Shape.Size = new Point(newWidth, background.Shape.Height);
    text = getTimerText();
    textDrawPos = GetTextCenterDrawPosition(_renderManager, text, background);
  }

  public void Draw(RenderManager _renderManager)
  {
    background.Draw(_renderManager);
    fill.Draw(_renderManager);
    _renderManager.DrawString(text, textDrawPos, Color.White);
  }

  // --- HELPERS --- //

  private string getTimerText()
  {
    return (int)(progress * 100) + " %";
  }

  // --- SET --- //
  public void SetSize(Point _size)
  {
    background.Shape.Size = _size;
    fill.Shape.Size = _size;
  }
}
