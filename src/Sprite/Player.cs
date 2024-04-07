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

  public void LoadContent(RenderManager _renderManager, string _texturePath, Map _map)
  {
    base.LoadContent(_renderManager, _texturePath);
    SetBounds(Vector2.Zero, new Vector2(_map.GetMapDimensionsPixels().X - rectangle.Width, _map.GetMapDimensionsPixels().Y - rectangle.Height));
  }

  public void Update(GameTime gameTime, Map _map)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    Vector2 newPos = handleMovement(elapsedTime);

    // Handle collision with map tiles
    Rectangle newPlayerRect = getPlayerRectangle(newPos);
    Tile[] tiles = _map.GetCollisionTiles();

    tiles
      .Where(tile => tile.GetRectangle().Intersects(newPlayerRect))
      .ToList()
      .ForEach(tile =>
      {
        Rectangle tileRect = tile.GetRectangle();
        if (newPlayerRect.Bottom > tileRect.Top && newPlayerRect.Top < tileRect.Top)
        {
          newPos.Y = position.Y;
        }
        else if (newPlayerRect.Top < tileRect.Bottom && newPlayerRect.Bottom > tileRect.Bottom)
        {
          newPos.Y = position.Y;
        }

        if (newPlayerRect.Right > tileRect.Left && newPlayerRect.Left < tileRect.Left)
        {
          newPos.X = position.X;
        }
        else if (newPlayerRect.Left < tileRect.Right && newPlayerRect.Right > tileRect.Right)
        {
          newPos.X = position.X;
        }
      });

    SetPosition(Vector2.Clamp(newPos, minPos, maxPos));
  }

  private Vector2 handleMovement(float elapsedTime)
  {
    bool left = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Left),
      right = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Right),
      up = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Up),
      down = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Down);

    Vector2 newPos = position;
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
      newPos.Y -= playerSpeed;
    }

    if (down)
    {
      newPos.Y += playerSpeed;
    }

    if (left)
    {
      newPos.X -= playerSpeed;

      if (InputManager.IsKeyPressed(Keys.Left))
      {
        ChangeState((int)PlayerStates.LEFT);
      }
    }

    if (right)
    {
      newPos.X += playerSpeed;

      if (InputManager.IsKeyPressed(Keys.Right))
      {
        ChangeState((int)PlayerStates.RIGHT);
      }
    }

    return newPos;
  }

  // Getters
  public float GetHealth() => health;

  private Rectangle getPlayerRectangle(Vector2 _newPos) =>
    new Rectangle((int)_newPos.X + rectangle.Width / 5, (int)_newPos.Y, rectangle.Width - rectangle.Width / 4, rectangle.Height);
}
