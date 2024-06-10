using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Enemy : AnimatedSprite
{

  public float Health { get; set; }
  public float Damage { get; set; }
  public bool IsDead { get; set; }
  public bool IsDying
  {
    get
    {
      return CurrentState == (int)EnemyStates.DYING;
    }
  }

  public const float MAX_HEALTH = 10f;

  public enum EnemyStates
  {
    DYING = 0,
    LEFT = 1,
    RIGHT = 2,
    UP = 3,
    DOWN = 4,
  }

  public const float BASE_SPEED = 50f;

  public Enemy(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    Geometry.CollisionTypes _collisionShape,
    int _totalStates,
    int _totalFrames,
    int _initialState,
    Point _tileSize,
    float _frameDelay = .125f,
    float _damage = 1f
  )
    : base(_renderManager, _texturePath, _position, _collisionShape, _totalStates, _totalFrames, _initialState, _tileSize, _frameDelay)
  {
    Health = MAX_HEALTH;
    Damage = _damage;
    IsDead = false;
  }

  public void Update(RenderManager _renderManager, GameTime gameTime, Map _map, Player _player)
  {
    if (IsDead) return;
    // Animation
    base.Update(gameTime);

    int newState = CurrentState;
    if (Health <= 0)
    {
      newState = (int)EnemyStates.DYING;
    }

    if (IsDying)
    {
      // Once dying animation is complete, enemy is dead
      if (CurrentFrame >= totalFrames - 1)
      {
        IsDead = true;
      }
      return;
    }

    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    handleMovement(_player, elapsedTime);

    handlePlayerCollision(_player);

    if (newState != CurrentState)
    {
      SetState(newState);
    }
  }

  // --- HELPERS --- //
  private void handleMovement(Player _player, float elapsedTime)
  {
    Vector2 playerCenter = _player.Shape.Center;
    float enemySpeed = BASE_SPEED * elapsedTime;
    Vector2 velocity = Vector2.Zero;

    // Move towards player
    if (playerCenter.X < Shape.Center.X)
    {
      velocity.X -= enemySpeed;
    }
    else if (playerCenter.X > Shape.Center.X)
    {
      velocity.X += enemySpeed;
    }

    if (playerCenter.Y < Shape.Position.Y)
    {
      velocity.Y -= enemySpeed;
    }
    else if (playerCenter.Y > Shape.Position.Y)
    {
      velocity.Y += enemySpeed;
    }

    // Change animation state depending on movement vector
    int newState = CurrentState;
    if (velocity.Y > 0 && Math.Abs(playerCenter.X - Shape.Center.X) < 75)
    {
      newState = (int)EnemyStates.DOWN;
    }
    else if (velocity.Y < 0 && Math.Abs(playerCenter.X - Shape.Center.X) < 75)
    {
      newState = (int)EnemyStates.UP;
    }
    else if (velocity.X > 0)
    {
      newState = (int)EnemyStates.RIGHT;
    }
    else if (velocity.X < 0)
    {
      newState = (int)EnemyStates.LEFT;
    }

    if (newState != CurrentState && !_player.Intersects(this))
    {
      SetState(newState);
    }

    Shape.Position += velocity;
  }

  private void handlePlayerCollision(Player _player)
  {
    if (_player.Intersects(this))
    {
      _player.TakeDamage(Damage);
    }
  }

  // --- SET --- //
  public void TakeDamage(float _damage) => Health -= _damage;
}
