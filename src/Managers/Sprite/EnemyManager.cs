using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class EnemyManager
{
  private readonly List<Enemy> spawnedEnemies;
  private readonly List<AnimatedPickup> spawnedPickups;
  private readonly int maxEnemies;
  private readonly float spawnDelay;
  private readonly string texturePath;

  private float timeSinceLastSpawn;

  public EnemyManager(int _maxEnemies, float _spawnDelay, string _texturePath)
  {
    maxEnemies = _maxEnemies;
    spawnDelay = _spawnDelay;
    texturePath = _texturePath;
    spawnedEnemies = new List<Enemy>();
    spawnedPickups = new List<AnimatedPickup>();
    timeSinceLastSpawn = 0;
  }

  public void Update(RenderManager _renderManager, GameTime _gameTime, Map _map, Player _player)
  {
    float elapsedTime = (float)_gameTime.ElapsedGameTime.TotalSeconds;
    timeSinceLastSpawn += elapsedTime;

    if (timeSinceLastSpawn >= spawnDelay && spawnedEnemies.Count < maxEnemies)
    {
      spawnedEnemies.Add(loadEnemy(_renderManager));
      timeSinceLastSpawn = 0;
    }

    List<Enemy> deadEnemies = new List<Enemy>();
    foreach (Enemy enemy in spawnedEnemies)
    {
      if (enemy.IsDead())
      {
        deadEnemies.Add(enemy);
        dropExpPickup(_renderManager, enemy);
        continue;
      }
      enemy.Update(_renderManager, _gameTime, _map, _player);
    }

    foreach (Enemy enemy in deadEnemies)
    {
      spawnedEnemies.Remove(enemy);
    }

    foreach (AnimatedPickup pickup in spawnedPickups)
    {
      if (pickup.IsDead())
      {
        spawnedPickups.Remove(pickup);
        continue;
      }
      pickup.Update(_renderManager, _gameTime);
    }
  }

  public void Draw(RenderManager _renderManager)
  {
    foreach (Enemy enemy in spawnedEnemies)
    {
      enemy.Draw(_renderManager);
    }

    foreach (AnimatedPickup pickup in spawnedPickups)
    {
      pickup.Draw(_renderManager);
    }
  }

  // --- HELPERS --- //

  // Creates a new enemy and loads its content
  private Enemy loadEnemy(RenderManager _renderManager)
  {
    return new Enemy(_renderManager, texturePath, Vector2.Zero, 4, 6, (int)Enemy.EnemyStates.RIGHT, new Point(48, 48));
  }

  private void dropExpPickup(RenderManager _renderManager, Enemy _enemy)
  {
    spawnedPickups.Add(new AnimatedPickup(_renderManager, "Sprites/exp_pickup", _enemy.GetCenter(), 1, 3, (int)AnimatedPickup.PickupStates.IDLE, new Point(16, 16)));
  }

  // -- GET -- //
  public List<Enemy> GetSpawnedEnemies() => spawnedEnemies;
}
