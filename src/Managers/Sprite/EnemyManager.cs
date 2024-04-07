using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class EnemyManager
{
  private readonly List<Enemy> spawnedEnemies;
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
    timeSinceLastSpawn = 0;
  }

  public void Update(RenderManager _renderManager, GameTime gameTime, Map _map, Player _player)
  {
    timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

    if (timeSinceLastSpawn >= spawnDelay && spawnedEnemies.Count < maxEnemies)
    {
      spawnedEnemies.Add(loadEnemy(_renderManager, _map));
      timeSinceLastSpawn = 0;
    }

    foreach (Enemy enemy in spawnedEnemies)
    {
      GameManager.Logger.LogConsole("Updating enemy");
      enemy.Update(gameTime, _map, _player);
    }
  }

  public void Draw(RenderManager _renderManager)
  {
    foreach (Enemy enemy in spawnedEnemies)
    {
      enemy.Draw(_renderManager);
    }
  }

  // Private helpers

  // Creates a new enemy and loads its content
  private Enemy loadEnemy(RenderManager _renderManager, Map _map)
  {
    Enemy enemy = new Enemy(Vector2.Zero, 2, 2, new Point(64, 64));
    enemy.LoadContent(_renderManager, texturePath, _map);
    return enemy;
  }
}
