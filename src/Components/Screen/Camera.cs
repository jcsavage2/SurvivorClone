using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace SurvivorClone;

public class Camera
{
  public Matrix translation { get; private set; }

  // TODO: this still isn't scaling the camera movment according to the window size
  public void Update(Player player, Map map)
  {
    var ratioX = RenderManager.WindowSize.X / RenderManager.RenderSize.X;
    var ratioY = RenderManager.WindowSize.Y / RenderManager.RenderSize.Y;
    var dx = (RenderManager.WindowSize.X / 2) - player.position.X * ratioX;
    dx = MathHelper.Clamp(dx, -map.mapDimensionsPixels.X + RenderManager.RenderSize.X + (map.tileSize / 2), map.tileSize / 2f);
    var dy = (RenderManager.WindowSize.Y / 2) - player.position.Y * ratioY;
    dy = MathHelper.Clamp(dy, -map.mapDimensionsPixels.Y + RenderManager.RenderSize.Y + (map.tileSize / 2), map.tileSize / 2f);
    translation = Matrix.CreateTranslation(dx, dy, 0f);
  }
}
