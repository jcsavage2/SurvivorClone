using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class ProgressBar : UIComponent
{
  private float progress;
  private readonly Sprite background;
  private readonly Sprite fill;

  public ProgressBar(RenderManager _renderManager, Vector2 _origin, float _verticalOffset, float _horizontalOffset, float _progress = 1)
    : base(_renderManager, _origin, _verticalOffset, _horizontalOffset)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);

    background = new Sprite(_renderManager, _origin);
    fill = new Sprite(_renderManager, _origin);
    background.LoadContent(_renderManager, "UI/back");
    fill.LoadContent(_renderManager, "UI/front");
  }

  public void Update(RenderManager _renderManager, float _progress)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);
    fill.UpdateDimensions(new Vector2(background.GetRectangle().Width * progress, background.GetRectangle().Height));
  }

  public void Draw(RenderManager _renderManager)
  {
    _renderManager
      .GetSpriteBatch()
      .Draw(background.GetTexture(), drawPosition, background.GetRectangle(), Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
    _renderManager
      .GetSpriteBatch()
      .Draw(fill.GetTexture(), drawPosition, fill.GetRectangle(), Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
    _renderManager
      .GetSpriteBatch()
      .DrawString(
        _renderManager.GetFont(),
        (int)(progress * 100) + " %",
        new Vector2(drawPosition.X + (background.GetTexture().Width / 2) - 75, drawPosition.Y + background.GetTexture().Height / 3),
        Color.White
      );
  }
}
