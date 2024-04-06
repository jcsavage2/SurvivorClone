using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class UIComponent
{
  protected Vector2 origin { get; set; }

  protected float verticalOffset { get; set; }
  private float horizontalOffset { get; set; }

  protected Vector2 drawPosition { get; set; }

  // horizontalOffset and verticalOffset are percentages of the window size pos or neg
  public UIComponent(RenderManager _renderManager, Vector2 _origin, float _verticalOffset, float _horizontalOffset)
  {
    origin = _origin;
    verticalOffset = _verticalOffset;
    horizontalOffset = _horizontalOffset;

    Vector2 computedOffset = new Vector2(horizontalOffset * _renderManager.GetRenderSize().X, verticalOffset * _renderManager.GetRenderSize().Y);
    drawPosition = new Vector2(origin.X + computedOffset.X, origin.Y + computedOffset.Y);
  }

  // Getters
  public Vector2 GetOrigin() => origin;

  public Vector2 GetDrawPosition() => drawPosition;

  public float GetVerticalOffset() => verticalOffset;

  public float GetHorizontalOffset() => horizontalOffset;
}
