using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SurvivorClone.Globals;

namespace SurvivorClone;

public class Player : Sprite
{
  // Projectiles
  private readonly List<Projectile> projectiles;
  private float lastFired;

  // State
  private int health = 10;

  // Constants
  public const float BASE_SPEED = 150f;
  public const float FIRE_RATE = 1f;

  public Player(Vector2 startPosition)
    : base(startPosition)
  {
    projectiles = new List<Projectile>();
    lastFired = 0;
  }

  public void Update(GameTime gameTime, List<Enemy> enemies)
  {
    var kstate = Keyboard.GetState();
    float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    handleMovement(elapsedTime, kstate);

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
      if (kstate.IsKeyDown(Keys.W))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Up));
      }
      else if (kstate.IsKeyDown(Keys.A))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Left));
      }
      else if (kstate.IsKeyDown(Keys.S))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Down));
      }
      else if (kstate.IsKeyDown(Keys.D))
      {
        lastFired = 0;
        projectiles.Add(new Projectile(position, Direction.Right));
      }
    }
  }

  // Moves the player based on keyboard input, handles collision with the window edges
  private void handleMovement(float elapsedTime, KeyboardState kstate)
  {
    bool left = kstate.IsKeyDown(Keys.Left),
      right = kstate.IsKeyDown(Keys.Right),
      up = kstate.IsKeyDown(Keys.Up),
      down = kstate.IsKeyDown(Keys.Down);

    Vector2 newPos = position;
    float playerSpeed = BASE_SPEED * elapsedTime;

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
    }

    if (right)
    {
      newPos.X += playerSpeed;
    }

    position = Vector2.Clamp(newPos, minPos, maxPos);
  }

  public void Draw(SpriteFont font)
  {
    base.Draw();

    Globals.SpriteBatch.DrawString(
      font,
      "Position: " + position.ToString() + " " + spriteTexture.Width,
      new Vector2(position.X + 15, position.Y - 15),
      Color.White
    );

    foreach (var projectile in projectiles)
    {
      projectile.Draw();
    }
  }
}
