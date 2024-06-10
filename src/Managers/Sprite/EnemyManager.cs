using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class EnemyManager
{
  public List<Enemy> SpawnedEnemies { get; set; }
  public List<AnimatedPickup> SpawnedPickups { get; set; }
  private readonly int maxEnemies;
  private readonly float spawnDelay;
  private readonly string texturePath;

  private float timeSinceLastSpawn;

  public EnemyManager(int _maxEnemies, float _spawnDelay, string _texturePath)
  {
    maxEnemies = _maxEnemies;
    spawnDelay = _spawnDelay;
    texturePath = _texturePath;
    SpawnedEnemies = new List<Enemy>();
    SpawnedPickups = new List<AnimatedPickup>();
    timeSinceLastSpawn = 0;
  }

  public void Update(RenderManager _renderManager, GameTime _gameTime, Map _map, Player _player)
  {
    float elapsedTime = (float)_gameTime.ElapsedGameTime.TotalSeconds;
    timeSinceLastSpawn += elapsedTime;

    if (timeSinceLastSpawn >= spawnDelay && SpawnedEnemies.Count < maxEnemies)
    {
      SpawnedEnemies.Add(loadEnemy(_renderManager));
      timeSinceLastSpawn = 0;
    }

    List<Enemy> deadEnemies = new List<Enemy>();
    foreach (Enemy enemy in SpawnedEnemies)
    {
      if (enemy.IsDead)
      {
        deadEnemies.Add(enemy);
        SpawnedPickups.Add(loadExpPickup(_renderManager, enemy));
        continue;
      }
      enemy.Update(_renderManager, _gameTime, _map, _player);
    }

    foreach (Enemy enemy in deadEnemies)
    {
      SpawnedEnemies.Remove(enemy);
    }

    foreach (AnimatedPickup pickup in SpawnedPickups)
    {
      if (pickup.IsDead)
      {
        SpawnedPickups.Remove(pickup);
        continue;
      }
      pickup.Update(_renderManager, _gameTime);
    }
  }

  public void Draw(RenderManager _renderManager)
  {
    foreach (Enemy enemy in SpawnedEnemies)
    {
      enemy.Draw(_renderManager);
    }

    foreach (AnimatedPickup pickup in SpawnedPickups)
    {
      pickup.Draw(_renderManager);
    }
  }

  // --- HELPERS --- //

  // Creates a new enemy and loads its content
  private Enemy loadEnemy(RenderManager _renderManager)
  {
    return new Enemy(_renderManager, texturePath, Vector2.Zero, Geometry.CollisionTypes.RECTANGLE, 4, 6, (int)Enemy.EnemyStates.RIGHT, new Point(48, 48));
  }

  private AnimatedPickup loadExpPickup(RenderManager _renderManager, Enemy _enemy)
  {
    return new AnimatedPickup(_renderManager, "Sprites/exp_pickup", _enemy.Shape.Center, Geometry.CollisionTypes.CIRCLE, 1, 3, (int)AnimatedPickup.PickupStates.IDLE, new Point(16, 16));
  }
}
