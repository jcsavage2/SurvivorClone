using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

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

  public Enemy(Vector2 _position, int _totalStates, int _totalFrames, Point _tileSize, float _frameDelay = .125f)
    : base(_position, _totalStates, _totalFrames, _tileSize, _frameDelay)
  {
    health = MAX_HEALTH;
  }

  public void LoadContent(RenderManager _renderManager, string _texturePath, Map _map)
  {
    base.LoadContent(_renderManager, _texturePath);
    SetBounds(Vector2.Zero, new Vector2(_map.GetMapDimensionsPixels().X - rectangle.Width, _map.GetMapDimensionsPixels().Y - rectangle.Height));
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
      ChangeState((int)EnemyStates.LEFT);
    }
    else if (playerPosition.X > position.X)
    {
      enemyPosition.X += enemySpeed;
      ChangeState((int)EnemyStates.RIGHT);
    }

    if (playerPosition.Y < position.Y)
    {
      enemyPosition.Y -= enemySpeed;
    }
    else if (playerPosition.Y > position.Y)
    {
      enemyPosition.Y += enemySpeed;
    }

    SetPosition(Vector2.Clamp(enemyPosition, minPos, maxPos));
  }

  // Getters
  public float GetHealth() => health;
}
