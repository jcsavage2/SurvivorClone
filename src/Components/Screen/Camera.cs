using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Camera
{
  public Matrix translation { get; private set; }

  public void Update(RenderManager _renderManager, Player _player, Map _map)
  {
    var cameraTranslationVector =
      new Vector3(-_player.GetPosition().X, -_player.GetPosition().Y, 0) * new Vector3(_renderManager.GetRatioX(), _renderManager.GetRatioY(), 0f); // Origin from player position
    var resTranslationVector = new Vector3(_renderManager.GetRenderSize().X / 2, _renderManager.GetRenderSize().Y / 2, 0); // Offset from origin to center the player in the middle of the screen

    var cameraTranslationMatrix = Matrix.CreateTranslation(cameraTranslationVector);
    var resTranslationMatrix = Matrix.CreateTranslation(resTranslationVector);

    translation = cameraTranslationMatrix * resTranslationMatrix;

    // var ratioX = RenderManager.WindowSize.X / RenderManager.RenderSize.X;
    // var ratioY = RenderManager.WindowSize.Y / RenderManager.RenderSize.Y;
    // var dx = (RenderManager.WindowSize.X / 2) - player.position.X * ratioX;
    // dx = MathHelper.Clamp(dx, -map.mapDimensionsPixels.X + RenderManager.WindowSize.X + (map.tileSize / 2), map.tileSize / 2f);
    // var dy = (RenderManager.WindowSize.Y / 2) - player.position.Y * ratioY;
    // dy = MathHelper.Clamp(dy, -map.mapDimensionsPixels.Y + RenderManager.WindowSize.Y + (map.tileSize / 2), map.tileSize / 2f);
    // translation = Matrix.CreateTranslation(dx, dy, 0f);
  }
}
