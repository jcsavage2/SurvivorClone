using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SurvivorClone.Globals;

namespace SurvivorClone;

public class Player : AnimatedSprite
{
  // Projectiles
  private readonly List<Projectile> projectiles;
  private float lastFired;

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

  public Player(Vector2 position, int _totalStates, int _totalFrames, Point _tileSize, float _frameDelay = .125f)
    : base(position, _totalStates, _totalFrames, _tileSize, _frameDelay)
  {
    projectiles = new List<Projectile>();
    lastFired = 0;
    Health = MAX_HEALTH;
  }

  public void Update(GameTime gameTime, List<Enemy> enemies)
  {
    base.Update(gameTime);
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    handleMovement(elapsedTime);

    foreach (var projectile in projectiles)
    {
      projectile.Update(gameTime);
    }

    // // Delete enemies and projectiles that intersect
    // List<Enemy> enemiesToRemove = new List<Enemy>();
    // List<Projectile> projectilesToRemove = new List<Projectile>();
    // foreach (var enemy in enemies)
    // {
    //   foreach (var projectile in projectiles.Where(p => p.rect.Intersects(enemy.rect)))
    //   {
    //     enemiesToRemove.Add(enemy);
    //     projectilesToRemove.Add(projectile);
    //   }
    // }

    // // Remove projectiles that are off screen
    // foreach (var projectile in projectiles.Where(p => p.rect.X < 0 || p.rect.X > WindowSize.X || p.rect.Y < 0 || p.rect.Y > WindowSize.Y))
    // {
    //   projectilesToRemove.Add(projectile);
    // }

    // // Reduce health for each enemy hit
    // foreach (var enemy in enemies.Where(e => e.rect.Intersects(rect)))
    // {
    //   health--;
    // }

    // enemies.RemoveAll(enemiesToRemove.Contains);
    // projectiles.RemoveAll(projectilesToRemove.Contains);

    // Shooting logic
    lastFired += elapsedTime;
    if (lastFired >= FIRE_RATE)
    {
      if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.W))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Up));
      }
      else if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.A))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Left));
      }
      else if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.S))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Down));
      }
      else if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.D))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Right));
      }
    }
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

      if (!InputManager.PreviousKeyboardState.IsKeyDown(Keys.Left))
      {
        ChangeState((int)PlayerStates.LEFT);
      }
    }

    if (right)
    {
      newPos.X += playerSpeed;

      if (!InputManager.PreviousKeyboardState.IsKeyDown(Keys.Right))
      {
        ChangeState((int)PlayerStates.RIGHT);
      }
    }

    position = Vector2.Clamp(newPos, minPos, maxPos);
  }

  public void Draw(SpriteFont font)
  {
    base.Draw();
    foreach (var projectile in projectiles)
    {
      projectile.Draw();
    }
  }
}
