using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class UIComponent
{
  protected Vector2 position { get; set; }

  protected int verticalOffset { get; set; }
  protected int horizontalOffset { get; set; }

  // horizontalOffset and verticalOffset are percentages of the window size pos or neg
  public UIComponent(RenderManager _renderManager, Vector2 _origin, int _verticalOffset, int _horizontalOffset)
  {
    verticalOffset = _verticalOffset;
    horizontalOffset = _horizontalOffset;
    position = new Vector2(_origin.X + horizontalOffset, _origin.Y + verticalOffset);
  }

  // --- GET --- //

  // Gets the draw position for a text centered in a texture background
  public Vector2 GetTextCenterDrawPosition(RenderManager _renderManager, string _text, Sprite _background)
  {
    Vector2 backgroundCenter = position + new Vector2(_background.GetSize().X / 2, _background.GetSize().Y / 2);
    Vector2 textSize = _renderManager.GetFont().MeasureString(_text);
    return new Vector2(backgroundCenter.X - textSize.X / 2, backgroundCenter.Y - textSize.Y / 2);
  }

  public float GetVerticalOffset() => verticalOffset;

  public float GetHorizontalOffset() => horizontalOffset;
}
