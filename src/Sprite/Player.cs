using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class Player : AnimatedSprite
{
  // State
  private float health { get; set; }

  // Constants
  public const float BASE_SPEED = 150f;
  public const float FIRE_RATE = 1f;
  public const float MAX_HEALTH = 10f;

  private enum PlayerStates
  {
    LEFT = 0,
    RIGHT = 1,
  }

  public Player(Vector2 _position, int _totalStates, int _totalFrames, Point _tileSize, float _frameDelay = .125f)
    : base(_position, _totalStates, _totalFrames, _tileSize, _frameDelay)
  {
    health = MAX_HEALTH;
  }

  public void Update(RenderManager _renderManager, GameTime gameTime, Map _map)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    Vector2 velocity = handleMovement(elapsedTime);
    Vector2 newPos = handleTileCollision(_map, GetNewPosition(velocity));

    SetPosition(newPos);
  }

  // --- HELPERS --- //

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

      if (InputManager.IsKeyPressed(Keys.Left))
      {
        SetState((int)PlayerStates.LEFT);
      }
    }

    if (right)
    {
      velocity.X = playerSpeed;

      if (InputManager.IsKeyPressed(Keys.Right))
      {
        SetState((int)PlayerStates.RIGHT);
      }
    }

    return velocity;
  }

  // --- GET --- //
  public float GetHealth() => health;
}
