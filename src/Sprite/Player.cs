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

  public Player(RenderManager _renderManager, Vector2 _position, int _totalStates, int _totalFrames, Point _tileSize, float _frameDelay = .125f)
    : base(_renderManager, _position, _totalStates, _totalFrames, _tileSize, _frameDelay)
  {
    health = MAX_HEALTH;
  }

  public void Update(GameTime gameTime, Map _map)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    bool left = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Left),
      right = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Right),
      up = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Up),
      down = InputManager.CurrentKeyboardState.IsKeyDown(Keys.Down);

    Vector2 newPos = GetPosition();
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

    SetBounds(Vector2.Zero, new Vector2(_map.GetMapDimensionsPixels().X - GetSize().X, _map.GetMapDimensionsPixels().Y - GetSize().Y));
    SetPosition(Vector2.Clamp(newPos, GetMinPos(), GetMaxPos()));
  }

  // Getters
  public float GetHealth() => health;
}
