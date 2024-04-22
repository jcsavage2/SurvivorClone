using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Enemy : AnimatedSprite
{
  // State
  private float health { get; set; }
  private float damage { get; set; }
  private bool isDead { get; set; }

  // Constants
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
    int _totalStates,
    int _totalFrames,
    int _initialState,
    Point _tileSize,
    float _frameDelay = .125f,
    float _damage = 1f
  )
    : base(_renderManager, _texturePath, _position, _totalStates, _totalFrames, _initialState, _tileSize, _frameDelay)
  {
    health = MAX_HEALTH;
    damage = _damage;
    isDead = false;
  }

  public void Update(RenderManager _renderManager, GameTime gameTime, Map _map, Player _player)
  {
    if (IsDead()) return;
    // Animation
    base.Update(gameTime);

    int newState = currentState;
    if (health <= 0)
    {
      newState = (int)EnemyStates.DYING;
    }

    if (IsDying())
    {
      // Once dying animation is complete, enemy is dead
      if (currentFrame >= totalFrames - 1)
      {
        isDead = true;
      }
      return;
    }

    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    handleMovement(_player, elapsedTime);

    handlePlayerCollision(_player);

    if (newState != currentState)
    {
      SetState(newState);
    }
  }

  // --- HELPERS --- //
  private void handleMovement(Player _player, float elapsedTime)
  {
    Vector2 playerCenter = _player.GetCenter(), center = GetCenter();
    float enemySpeed = BASE_SPEED * elapsedTime;
    Vector2 velocity = Vector2.Zero;

    // Move towards player
    if (playerCenter.X < center.X)
    {
      velocity.X -= enemySpeed;
    }
    else if (playerCenter.X > center.X)
    {
      velocity.X += enemySpeed;
    }

    if (playerCenter.Y < position.Y)
    {
      velocity.Y -= enemySpeed;
    }
    else if (playerCenter.Y > position.Y)
    {
      velocity.Y += enemySpeed;
    }

    // Change animation state depending on movement vector
    int newState = currentState;
    if (velocity.Y > 0 && Math.Abs(playerCenter.X - center.X) < 75)
    {
      newState = (int)EnemyStates.DOWN;
    }
    else if (velocity.Y < 0 && Math.Abs(playerCenter.X - center.X) < 75)
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

    if (newState != currentState && !_player.GetBoundingBox().Intersects(GetBoundingBox()))
    {
      SetState(newState);
    }

    SetPosition(position + velocity);
  }

  private void handlePlayerCollision(Player _player)
  {
    if (_player.GetBoundingBox().Intersects(GetBoundingBox()))
    {
      _player.TakeDamage(damage);
    }
  }

  // --- SET --- //
  public void TakeDamage(float _damage) => health -= _damage;

  // --- GET --- //
  public float GetHealth() => health;

  public float GetDamage() => damage;
  public bool IsDead() => isDead;
  public bool IsDying() => currentState == (int)EnemyStates.DYING;
}
