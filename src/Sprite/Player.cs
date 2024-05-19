using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class Player : AnimatedSprite
{
  // State
  private float health { get; set; }
  private List<Projectile> projectiles { get; set; }
  private float timeSinceLastFire { get; set; }

  // Constants
  public const float BASE_SPEED = 150f;
  public const float FIRE_RATE = 1f;
  public const float MAX_HEALTH = 100f;

  public enum PlayerStates
  {
    LEFT = 0,
    RIGHT = 1,
  }

  public Player(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    int _totalStates,
    int _totalFrames,
    int _initialState,
    Point _tileSize,
    float _frameDelay = .125f
  )
    : base(_renderManager, _texturePath, _position, _totalStates, _totalFrames, _initialState, _tileSize, _frameDelay)
  {
    health = MAX_HEALTH;
    projectiles = new List<Projectile>();
  }

  public void Update(RenderManager _renderManager, EnemyManager _enemyManager, GameTime gameTime, Map _map)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    Vector2 velocity = handleMovement(elapsedTime);
    Vector2 newPos = handleTileCollision(_map, GetNewPosition(velocity));

    handleAttack(_renderManager, elapsedTime, _enemyManager, _map);

    SetPosition(newPos);
  }

  public override void Draw(RenderManager _renderManager)
  {
    base.Draw(_renderManager);
    foreach (Projectile projectile in projectiles)
    {
      projectile.Draw(_renderManager);
    }
  }

  // --- HELPERS --- //

  // Handles player attack logic
  private void handleAttack(RenderManager _renderManager, float elapsedTime, EnemyManager _enemyManager, Map _map)
  {
    timeSinceLastFire += elapsedTime;
    if (timeSinceLastFire >= FIRE_RATE)
    {
      timeSinceLastFire = 0;
      projectiles.Add(new Projectile(_renderManager, "Sprites/base_projectile", GetCenterLeft(), Projectile.Origin.LEFT, Projectile.Direction.LEFT, 1, 100));
    }

    List<Projectile> deadProjectiles = new List<Projectile>();
    List<Enemy> spawnedEnemies = _enemyManager.GetSpawnedEnemies();
    foreach (Projectile projectile in projectiles)
    {
      projectile.Update(_renderManager, elapsedTime, _map, spawnedEnemies);
      if (projectile.IsDead())
      {
        deadProjectiles.Add(projectile);
      }
    }

    foreach (Projectile projectile in deadProjectiles)
    {
      projectiles.Remove(projectile);
    }
  }

  // Returns an updated position based on collision with map tiles
  private Vector2 handleTileCollision(Map _map, Vector2 _pos)
  {
    // Handle collision with map tiles
    Rectangle newPlayerRect = GetBoundingBox(_pos),
      oldPlayerRect = GetBoundingBox();
    Tile[] tiles = _map.GetCollisionTiles();

    tiles
      .Where(tile => tile.GetBoundingBox().Intersects(newPlayerRect))
      .ToList()
      .ForEach(tile =>
      {
        Rectangle tileRect = tile.GetBoundingBox();
        // Check if player is above or below the tile to determine which axis to correct
        if (oldPlayerRect.Bottom <= tileRect.Top || oldPlayerRect.Top >= tileRect.Bottom)
        {
          if (newPlayerRect.Bottom > tileRect.Top && newPlayerRect.Top < tileRect.Top)
          {
            _pos.Y = tileRect.Top - newPlayerRect.Height;
          }
          else if (newPlayerRect.Top < tileRect.Bottom && newPlayerRect.Bottom > tileRect.Bottom)
          {
            _pos.Y = tileRect.Bottom;
          }
        }
        else
        {
          if (newPlayerRect.Right > tileRect.Left && newPlayerRect.Left < tileRect.Left)
          {
            _pos.X = tileRect.Left - newPlayerRect.Width;
          }
          else if (newPlayerRect.Left < tileRect.Right && newPlayerRect.Right > tileRect.Right)
          {
            _pos.X = tileRect.Right;
          }
        }
      });

    return _pos;
  }

  // Calculates player velocity based on key input
  private Vector2 handleMovement(float elapsedTime)
  {
    bool left = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Left),
      right = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Right),
      up = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Up),
      down = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Down);

    Vector2 velocity = Vector2.Zero;
    float playerSpeed = BASE_SPEED * elapsedTime;

    if (!up && !down && !left && !right)
    {
      StopAnimation();
    }
    else
    {
      StartAnimation();
    }

    if (up)
    {
      velocity.Y = -playerSpeed;
    }

    if (down)
    {
      velocity.Y = playerSpeed;
    }

    if (left)
    {
      velocity.X = -playerSpeed;
    }

    if (right)
    {
      velocity.X = playerSpeed;
    }

    if (velocity.X > 0 && currentState != (int)PlayerStates.RIGHT)
    {
      SetState((int)PlayerStates.RIGHT);
    }
    else if (velocity.X < 0 && currentState != (int)PlayerStates.LEFT)
    {
      SetState((int)PlayerStates.LEFT);
    }

    return velocity;
  }

  // --- SET --- //
  public void TakeDamage(float _damage) => health -= _damage;

  // --- GET --- //
  public float GetHealth() => health;
}
