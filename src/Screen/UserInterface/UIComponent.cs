using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class UIComponent
{
  public Vector2 Position { get; set; }

  protected int verticalOffset { get; set; }
  protected int horizontalOffset { get; set; }

  // horizontalOffset and verticalOffset are percentages of the window size pos or neg
  public UIComponent(RenderManager _renderManager, Vector2 _origin, int _verticalOffset, int _horizontalOffset)
  {
    verticalOffset = _verticalOffset;
    horizontalOffset = _horizontalOffset;
    Position = new Vector2(_origin.X + horizontalOffset, _origin.Y + verticalOffset);
  }

  // Gets the draw Position for a text centered in a texture background
  public Vector2 GetTextCenterDrawPosition(RenderManager _renderManager, string _text, Sprite _background)
  {
    Vector2 backgroundCenter = Position + new Vector2(_background.Shape.Width / 2, _background.Shape.Height / 2);
    Vector2 textSize = _renderManager.Font.MeasureString(_text);
    return new Vector2(backgroundCenter.X - textSize.X / 2, backgroundCenter.Y - textSize.Y / 2);
  }
}
