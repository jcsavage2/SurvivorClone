using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Camera
{
  public Matrix translation { get; private set; }

  public void Update(RenderManager _renderManager, Player _player, Map _map)
  {
    Vector3 playerPosition = new Vector3(_player.GetCenter().X, _player.GetCenter().Y, 0);
    Point renderSize = _renderManager.GetRenderSize();

    int offset = 0;
    if (Debug.IsActive())
      offset = 10;

    int minX = renderSize.X / 2 - offset;
    int maxX = _map.GetMapDimensionsPixels().X - renderSize.X / 2 + offset;
    int minY = renderSize.Y / 2 - offset;
    int maxY = _map.GetMapDimensionsPixels().Y - renderSize.Y / 2 + offset;

    // Clamp the camera center to the borders of the map
    float clampedX = MathHelper.Clamp(playerPosition.X, minX, maxX);
    float clampedY = MathHelper.Clamp(playerPosition.Y, minY, maxY);

    Vector3 cameraTranslationVector = new Vector3(-clampedX, -clampedY, 0);

    // Offset by half the screen size to center the camera
    Vector3 resTranslationVector = new Vector3(renderSize.X / 2, renderSize.Y / 2, 0);

    Matrix cameraTranslationMatrix = Matrix.CreateTranslation(cameraTranslationVector);
    Matrix resTranslationMatrix = Matrix.CreateTranslation(resTranslationVector);

    translation = cameraTranslationMatrix * resTranslationMatrix;
  }
}
