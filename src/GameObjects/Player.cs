using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class Player : AnimatedSprite
{
  public float Health { get; set; }
  public List<Projectile> Projectiles { get; set; }
  private float timeSinceLastFire { get; set; }
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
    Geometry.CollisionTypes _collisionType,
    int _totalStates,
    int _totalFrames,
    int _initialState,
    Point _size,
    float _frameDelay = .125f
  )
    : base(_renderManager, _texturePath, _position, _collisionType, _totalStates, _totalFrames, _initialState, _size, _frameDelay)
  {
    Health = MAX_HEALTH;
    Projectiles = new List<Projectile>();
  }

  public void Update(RenderManager _renderManager, EnemyManager _enemyManager, GameTime gameTime, Map _map)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    Vector2 velocity = handleMovement(elapsedTime);
    Vector2 newPos = handleTileCollision(_map, Shape.Position + velocity);

    handleAttack(_renderManager, elapsedTime, _enemyManager, _map);

    Shape.Position = newPos;
  }

  public override void Draw(RenderManager _renderManager)
  {
    base.Draw(_renderManager);
    foreach (Projectile projectile in Projectiles)
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
      Projectiles.Add(new Projectile(_renderManager, "Sprites/base_projectile", Shape.CenterLeft, Geometry.CollisionTypes.RECTANGLE, Projectile.DirectionType.LEFT, 1, 100));
    }

    List<Projectile> deadProjectiles = new List<Projectile>();
    List<Enemy> spawnedEnemies = _enemyManager.SpawnedEnemies;
    foreach (Projectile projectile in Projectiles)
    {
      projectile.Update(_renderManager, elapsedTime, _map, spawnedEnemies);
      if (projectile.IsDead)
      {
        deadProjectiles.Add(projectile);
      }
    }

    foreach (Projectile projectile in deadProjectiles)
    {
      Projectiles.Remove(projectile);
    }
  }

  // Returns an updated position based on collision with map tiles
  private Vector2 handleTileCollision(Map _map, Vector2 _newPos)
  {
    // Handle collision with map tiles
    Rectangle newPlayerRect = new Rectangle(_newPos, Shape.Size);

    _map.CollisionTiles
      .Where(tile => tile.Intersects(newPlayerRect))
      .ToList()
      .ForEach(tile =>
      {
        // Check if player is above or below the tile to determine which axis to correct
        if (Shape.Bottom <= tile.Shape.Top || Shape.Top >= tile.Shape.Bottom)
        {
          if (newPlayerRect.Bottom > tile.Shape.Top && newPlayerRect.Top < tile.Shape.Top)
          {
            _newPos.Y = tile.Shape.Top - newPlayerRect.Height;
          }
          else if (newPlayerRect.Top < tile.Shape.Bottom && newPlayerRect.Bottom > tile.Shape.Bottom)
          {
            _newPos.Y = tile.Shape.Bottom;
          }
        }
        else
        {
          if (newPlayerRect.Right > tile.Shape.Left && newPlayerRect.Left < tile.Shape.Left)
          {
            _newPos.X = tile.Shape.Left - newPlayerRect.Width;
          }
          else if (newPlayerRect.Left < tile.Shape.Right && newPlayerRect.Right > tile.Shape.Right)
          {
            _newPos.X = tile.Shape.Right;
          }
        }
      });

    return _newPos;
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

    if (velocity.X > 0 && CurrentState != (int)PlayerStates.RIGHT)
    {
      SetState((int)PlayerStates.RIGHT);
    }
    else if (velocity.X < 0 && CurrentState != (int)PlayerStates.LEFT)
    {
      SetState((int)PlayerStates.LEFT);
    }

    return velocity;
  }

  // --- SET --- //
  public void TakeDamage(float _damage) => Health -= _damage;
}
