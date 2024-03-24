using Microsoft.Xna.Framework;
using static SurvivorClone.Player;

namespace SurvivorClone;

public class Projectile : Sprite
{
  private readonly float projectileSpeed;
  private readonly Direction direction;

  public Projectile(Vector2 startPosition, Direction _direction, float _projectileSpeed = 250f, string texturePath = "projectile")
    : base(startPosition)
  {
    this.projectileSpeed = _projectileSpeed;
    this.direction = _direction;
    base.LoadContent(texturePath);
  }

  public void Update(GameTime gameTime)
  {
    float Y = position.Y,
      X = position.X;
    float dist = projectileSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

    switch (direction)
    {
      case Direction.Up:
        Y -= dist;
        break;
      case Direction.Down:
        Y += dist;
        break;
      case Direction.Left:
        X -= dist;
        break;
      case Direction.Right:
        X += dist;
        break;
    }
    position = new Vector2(X, Y);
  }
}
