using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Projectile : Sprite
{
  // State
  private readonly Direction direction;
  private float damage { get; set; }
  private bool isDead { get; set; }
  private int durability { get; set; }


  public enum Direction
  {
    LEFT = 1,
    RIGHT = 2,
    UP = 3,
    DOWN = 4,
  }

  public enum Origin { TOP = 1, BOTTOM = 2, LEFT = 3, RIGHT = 4 }

  public const float BASE_SPEED = 150f;

  public Projectile(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    Origin _origin,
    Direction _direction,
    int _durability,
    float _damage
  )
    : base(_renderManager, _texturePath, _position)
  {
    isDead = false;
    direction = _direction;
    durability = _durability;
    damage = _damage;
  }

  public void Update(RenderManager _renderManager, float _elapsedTime, Map _map, List<Enemy> _enemies)
  {
    // Move projectile
    Vector2 velocity = getVelocity(_elapsedTime);
    SetPosition(GetNewPosition(velocity));

    // Check for collision with enemies and damage accordingly
    foreach (Enemy enemy in _enemies)
    {
      if (enemy.GetBoundingBox().Intersects(GetBoundingBox()))
      {
        enemy.TakeDamage(damage);
        --durability;
        if (durability <= 0)
        {
          isDead = true;
          break;
        }
      }
    }

    // Check for collision with map borders
    if (position.X < 0 || position.X > _map.GetMapDimensionsPixels().X || position.Y < 0 || position.Y > _map.GetMapDimensionsPixels().Y)
    {
      isDead = true;
    }
  }

  // --- HELPERS --- //
  private Vector2 getVelocity(float _elapsedTime)
  {
    Vector2 velocity = Vector2.Zero;
    float speed = BASE_SPEED * _elapsedTime;

    switch (direction)
    {
      case Direction.LEFT:
        velocity.X = -speed;
        break;
      case Direction.RIGHT:
        velocity.X = speed;
        break;
      case Direction.UP:
        velocity.Y = -speed;
        break;
      case Direction.DOWN:
        velocity.Y = speed;
        break;
    }

    return velocity;
  }

  // --- GET --- //
  public bool IsDead() => isDead;

  // --- HELPERS --- //
}
