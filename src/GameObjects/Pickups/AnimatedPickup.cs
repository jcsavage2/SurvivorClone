using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class AnimatedPickup : AnimatedSprite
{
  public bool IsDead
  {
    get
    {
      return CurrentState == (int)PickupStates.DEAD;
    }
  }

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
    Geometry.CollisionTypes _collisionShape,
    int _totalStates,
    int _totalFrames,
    int _initialState,
    Point _size,
    float _frameDelay = .125f
  )
    : base(_renderManager, _texturePath, _position, _collisionShape, _totalStates, _totalFrames, _initialState, _size, _frameDelay) { }

  public void Update(RenderManager _renderManager, GameTime _elapsedTime)
  {
    base.Update(_elapsedTime);
  }
}
