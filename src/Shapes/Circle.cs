using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Circle : Shape
{
  public override Point Size { get; set; }
  public override Vector2 Position { get; set; }
  public override int Bottom => (int)Position.Y + Size.Y;
  public override int Left => (int)Position.X;
  public override int Right => (int)Position.X + Size.X;
  public override int Top => (int)Position.Y;

  private float Radius
  {
    get
    {
      return Size.X / 2f;
    }
  }

  public Circle(Vector2 _position, Point _size)
  {
    Position = _position;
    Size = _size;
  }

  public Circle(int _x, int _y, int _width)
  {
    Position = new Vector2(_x, _y);
    Size = new Point(_width, _width);
  }

  public override bool IntersectsWith(Shape _shape)
  {
    return _shape.Intersects(this);
  }

  public override bool Intersects(Rectangle _rectangle)
  {
    float closestX = Math.Max(_rectangle.Position.X, Math.Min(Position.X, _rectangle.Position.X + _rectangle.Width));
    float closestY = Math.Max(_rectangle.Position.Y, Math.Min(Position.Y, _rectangle.Position.Y + _rectangle.Height));

    float distance = Geometry.EuclideanDistance(Position.X, Position.Y, (int)closestX, (int)closestY);
    return distance < Radius;
  }

  public override bool Intersects(Circle _circle)
  {
    float distance = Geometry.EuclideanDistance(Position.X, Position.Y, _circle.Position.X, _circle.Position.Y);
    return distance < Radius + _circle.Radius;
  }
}
