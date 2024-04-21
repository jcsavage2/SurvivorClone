using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Enemy : AnimatedSprite
{
  // State
  private float health { get; set; }
  private float damage { get; set; }

  // Constants
  public const float MAX_HEALTH = 10f;

  private enum EnemyStates
  {
    LEFT = 0,
    RIGHT = 1,
    UP = 2,
    DOWN = 3,
  }

  public const float BASE_SPEED = 50f;

  public Enemy(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    int _totalStates,
    int _totalFrames,
    Point _tileSize,
    float _frameDelay = .125f,
    float _damage = 1f
  )
    : base(_renderManager, _texturePath, _position, _totalStates, _totalFrames, _tileSize, _frameDelay)
  {
    health = MAX_HEALTH;
    damage = _damage;
  }

  public void Update(RenderManager _renderManager, GameTime gameTime, Map _map, Player _player)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    Vector2 playerPosition = _player.GetCenter(), center = GetCenter();
    float enemySpeed = BASE_SPEED * elapsedTime;
    Vector2 velocity = Vector2.Zero;

    // Move towards player
    if (playerPosition.X < center.X)
    {
      velocity.X -= enemySpeed;
    }
    else if (playerPosition.X > center.X)
    {
      velocity.X  += enemySpeed;
    }

    if (playerPosition.Y < position.Y)
    {
      velocity.Y  -= enemySpeed;
    }
    else if (playerPosition.Y > position.Y)
    {
      velocity.Y += enemySpeed;
    }

    int newState = -1;
    if (velocity.Y > 0 && Math.Abs(playerPosition.X - center.X) < 75){
      newState = (int)EnemyStates.DOWN;
    } else if (velocity.Y < 0 && Math.Abs(playerPosition.X - center.X) < 75){
      newState = (int)EnemyStates.UP;
    } else if (velocity.X > 0){
      newState = (int)EnemyStates.RIGHT;
    } else if (velocity.X < 0){
      newState = (int)EnemyStates.LEFT;
    }

    if (newState != -1 && newState != currentState && !_player.GetBoundingBox().Intersects(GetBoundingBox()))
    {
      SetState(newState);
    }

    SetPosition(position + velocity);
  }

  // --- GET --- //
  public float GetHealth() => health;

  public float GetDamage() => damage;
}
