using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class ProgressBar : UIComponent
{
  private float progress;
  private readonly Sprite background;
  private readonly Sprite fill;

  public ProgressBar(Vector2 _origin, float _verticalOffset, float _horizontalOffset, float _progress = 1)
    : base(_origin, _verticalOffset, _horizontalOffset)
  {
    progress = MathHelper.Clamp(_progress, 0, 1);

    background = new Sprite(origin);
    fill = new Sprite(origin);
    background.LoadContent("UI/back");
    fill.LoadContent("UI/front");
  }

  public void Update(float _progress)
  {
    base.Update();
    progress = MathHelper.Clamp(_progress, 0, 1);
    fill.UpdateDimensions(new Vector2(background.rectangle.Width * progress, background.rectangle.Height));
  }

  public void Draw()
  {
    RenderManager.SpriteBatch.Draw(
      background.spriteTexture,
      drawPosition,
      background.rectangle,
      Color.White,
      0f,
      Vector2.Zero,
      Vector2.One,
      SpriteEffects.None,
      0f
    );
    RenderManager.SpriteBatch.Draw(
      fill.spriteTexture,
      drawPosition,
      fill.rectangle,
      Color.White,
      0f,
      Vector2.Zero,
      Vector2.One,
      SpriteEffects.None,
      0f
    );
    RenderManager.SpriteBatch.DrawString(
      RenderManager.Font,
      (int)(progress * 100) + " %",
      new Vector2(drawPosition.X + (background.spriteTexture.Width / 2) - 75, drawPosition.Y + background.spriteTexture.Height / 3),
      Color.White
    );
  }
}
