using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Camera
{
  public Matrix translation { get; private set; }

  // Update camera position based on player position, clamp to map boundaries
  public void Update(RenderManager _renderManager, Player _player, Map _map)
  {
    var playerPosition =
      new Vector3(_player.GetPosition().X, _player.GetPosition().Y, 0) * new Vector3(_renderManager.GetRatioX(), _renderManager.GetRatioY(), 0f);
    var renderSize = _renderManager.GetRenderSize();

    var minX = renderSize.X / 2 - 32;
    var maxX = _map.GetMapDimensionsPixels().X - renderSize.X / 2 - 32;
    var minY = renderSize.Y / 2 - 32;
    var maxY = _map.GetMapDimensionsPixels().Y - renderSize.Y / 2 - 32;

    var clampedX = MathHelper.Clamp(playerPosition.X, minX, maxX);
    var clampedY = MathHelper.Clamp(playerPosition.Y, minY, maxY);

    var cameraTranslationVector = new Vector3(-clampedX, -clampedY, 0);
    var resTranslationVector = new Vector3(renderSize.X / 2, renderSize.Y / 2, 0);

    var cameraTranslationMatrix = Matrix.CreateTranslation(cameraTranslationVector);
    var resTranslationMatrix = Matrix.CreateTranslation(resTranslationVector);

    translation = cameraTranslationMatrix * resTranslationMatrix;
  }
}
