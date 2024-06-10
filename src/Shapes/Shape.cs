using Microsoft.Xna.Framework;

namespace SurvivorClone;

public abstract class Shape
{
  public abstract Point Size { get; set; }
  public abstract Vector2 Position { get; set; }
  public abstract int Bottom { get; }
  public abstract int Left { get; }
  public abstract int Right { get; }
  public abstract int Top { get; }
  public int Width { get => Size.X; }
  public int Height { get => Size.Y; }

  public Vector2 Center => new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2);
  // Get center point on each edge
  public Vector2 CenterLeft => new Vector2(Position.X, Position.Y + Size.Y / 2);
  public Vector2 CenterRight => new Vector2(Position.X + Size.X, Position.Y + Size.Y / 2);
  public Vector2 CenterTop => new Vector2(Position.X + Size.X / 2, Position.Y);
  public Vector2 CenterBottom => new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y);

  public abstract bool IntersectsWith(Shape _shape);

  public abstract bool Intersects(Rectangle _rectangle);
  public abstract bool Intersects(Circle _circle);

  public static Shape CreateShape(Vector2 _position, Point _size, Geometry.CollisionTypes _collisionType)
  {
    if (_collisionType == Geometry.CollisionTypes.RECTANGLE)
    {
      return new Rectangle(_position, _size);
    }
    else
    {
      if (_size.X != _size.Y)
      {
        Debug.ThrowError($"Circle shape must have equal width and height. Width: {_size.X}, Height: {_size.Y}");
      }
      return new Circle(_position, _size);
    }
  }
}