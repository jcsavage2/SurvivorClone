using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class AnimatedPickup : AnimatedSprite
{

  public enum PickupStates
  {
    IDLE = 0,
    PICKING_UP = 1,
    DEAD = 2,
  }

  public AnimatedPickup(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    int _totalStates,
    int _totalFrames,
    int _initialState,
    Point _tileSize,
    float _frameDelay = .125f
  )
    : base(_renderManager, _texturePath, _position, _totalStates, _totalFrames, _initialState, _tileSize, _frameDelay) { }

  public void Update(RenderManager _renderManager, GameTime _elapsedTime)
  {
    base.Update(_elapsedTime);
  }

  // --- HELPERS --- //

  // --- GET --- //
  public bool IsDead() => currentState == (int)PickupStates.DEAD;
}
