using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Projectile : Sprite
{
  public DirectionType Direction { get; set; }
  public float Damage { get; set; }
  public bool IsDead { get; set; }
  private int durability { get; set; }

  public enum DirectionType
  {
    LEFT = 1,
    RIGHT = 2,
    UP = 3,
    DOWN = 4,
  }

  public const float BASE_SPEED = 150f;

  public Projectile(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    Geometry.CollisionTypes _collisionType,
    DirectionType _direction,
    int _durability,
    float _damage
  )
    : base(_renderManager, _texturePath, _position, _collisionType)
  {
    IsDead = false;
    Direction = _direction;
    durability = _durability;
    Damage = _damage;
  }

  public void Update(RenderManager _renderManager, float _elapsedTime, Map _map, List<Enemy> _enemies)
  {
    // Move projectile
    Vector2 velocity = getVelocity(_elapsedTime);
    Shape.Position += velocity;

    // Check for collision with enemies and damage accordingly
    foreach (Enemy enemy in _enemies)
    {
      if (enemy.Intersects(this))
      {
        enemy.TakeDamage(Damage);
        --durability;
        if (durability <= 0)
        {
          IsDead = true;
          break;
        }
      }
    }

    // Check for collision with map borders
    if (Shape.Position.X < 0 || Shape.Position.X > _map.MapDimensionsPixels.X || Shape.Position.Y < 0 || Shape.Position.Y > _map.MapDimensionsPixels.Y)
    {
      IsDead = true;
    }
  }

  // --- HELPERS --- //
  private Vector2 getVelocity(float _elapsedTime)
  {
    Vector2 velocity = Vector2.Zero;
    float speed = BASE_SPEED * _elapsedTime;

    switch (Direction)
    {
      case DirectionType.LEFT:
        velocity.X = -speed;
        break;
      case DirectionType.RIGHT:
        velocity.X = speed;
        break;
      case DirectionType.UP:
        velocity.Y = -speed;
        break;
      case DirectionType.DOWN:
        velocity.Y = speed;
        break;
    }

    return velocity;
  }
}
