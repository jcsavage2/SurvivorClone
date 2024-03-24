using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class Player : AnimatedSprite
{
  // State
  public float Health { get; set; }

  // Constants
  public const float BASE_SPEED = 150f;
  public const float FIRE_RATE = 1f;
  public const float MAX_HEALTH = 10f;

  public enum PlayerStates
  {
    LEFT = 0,
    RIGHT = 1,
  }

  public enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }

  public Player(Vector2 position, int _totalStates, int _totalFrames, Point _tileSize, float _frameDelay = .125f)
    : base(position, _totalStates, _totalFrames, _tileSize, _frameDelay)
  {
    Health = MAX_HEALTH;
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    handleMovement(elapsedTime);
  }

  // Moves the player based on keyboard input, handles collision with the window edges
  private void handleMovement(float elapsedTime)
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

    position = Vector2.Clamp(newPos, minPos, maxPos);
  }

  public void Draw(SpriteFont font)
  {
    base.Draw();
  }
}
