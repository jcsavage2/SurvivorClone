using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Camera
{
  public Matrix translation { get; private set; }

  public void Follow(Player player, Map map)
  {
    var dx = (Globals.WindowSize.X / 2) - player.position.X;
    dx = MathHelper.Clamp(dx, -map.mapDimensionsPixels.X + Globals.WindowSize.X + (Globals.TILE_SIZE / 2), Globals.TILE_SIZE / 2f);
    var dy = (Globals.WindowSize.Y / 2) - player.position.Y;
    dy = MathHelper.Clamp(dy, -map.mapDimensionsPixels.Y + Globals.WindowSize.Y + (Globals.TILE_SIZE / 2), Globals.TILE_SIZE / 2f);
    translation = Matrix.CreateTranslation(dx, dy, 0f);
  }
}
