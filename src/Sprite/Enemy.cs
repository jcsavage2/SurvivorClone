using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Enemy : AnimatedSprite
{
  // State
  private float health { get; set; }

  // Constants
  public const float MAX_HEALTH = 10f;

  private enum EnemyStates
  {
    LEFT = 0,
    RIGHT = 1,
  }

  public const float BASE_SPEED = 50f;

  public Enemy(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    int _totalStates,
    int _totalFrames,
    Point _tileSize,
    float _frameDelay = .125f
  )
    : base(_renderManager, _texturePath, _position, _totalStates, _totalFrames, _tileSize, _frameDelay)
  {
    health = MAX_HEALTH;
  }

  public void Update(GameTime gameTime, Map _map, Player _player)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    Vector2 playerPosition = _player.GetPosition();
    Vector2 enemyPosition = position;
    float enemySpeed = BASE_SPEED * elapsedTime;

    // Move towards player
    if (playerPosition.X < position.X)
    {
      enemyPosition.X -= enemySpeed;
      SetState((int)EnemyStates.LEFT);
    }
    else if (playerPosition.X > position.X)
    {
      enemyPosition.X += enemySpeed;
      SetState((int)EnemyStates.RIGHT);
    }

    if (playerPosition.Y < position.Y)
    {
      enemyPosition.Y -= enemySpeed;
    }
    else if (playerPosition.Y > position.Y)
    {
      enemyPosition.Y += enemySpeed;
    }

    SetPosition(enemyPosition);
  }

  // --- GET --- //
  public float GetHealth() => health;
}
