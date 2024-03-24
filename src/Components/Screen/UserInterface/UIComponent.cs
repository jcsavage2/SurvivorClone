using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class UIComponent
{
  public Vector2 origin { get; set; }

  public float verticalOffset { get; set; }
  public float horizontalOffset { get; set; }

  public Vector2 drawPosition { get; set; }

  // horizontalOffset and verticalOffset are percentages of the window size pos or neg
  public UIComponent(Vector2 _origin, float _verticalOffset, float _horizontalOffset)
  {
    origin = _origin;
    verticalOffset = _verticalOffset;
    horizontalOffset = _horizontalOffset;
  }

  // Update the draw position of the UI component based on the window size
  public virtual void Update()
  {
    Vector2 computedOffset = new Vector2(horizontalOffset * RenderManager.RenderTarget.Width, verticalOffset * RenderManager.RenderTarget.Height);
    drawPosition = new Vector2(origin.X + computedOffset.X, origin.Y + computedOffset.Y);
  }
}
