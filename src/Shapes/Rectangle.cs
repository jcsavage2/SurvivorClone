using Microsoft.Xna.Framework;

namespace SurvivorClone;

public class Rectangle : Shape
{
  private Microsoft.Xna.Framework.Rectangle rectangle;
  private Vector2 _pos;

  public override Point Size
  {
    get => rectangle.Size;
    set => rectangle = new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, value.X, value.Y);

  }
  public override Vector2 Position
  {
    get => _pos;
    set
    {
      rectangle = new Microsoft.Xna.Framework.Rectangle((int)value.X, (int)value.Y, Width, Height);
      _pos = value;
    }
  }
  public override int Bottom => rectangle.Bottom;
  public override int Left => rectangle.Left;
  public override int Right => rectangle.Right;
  public override int Top => rectangle.Top;

  public Rectangle(Vector2 _position, Point _size)
  {
    rectangle = new Microsoft.Xna.Framework.Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y);
    _pos = _position;
  }

  public Rectangle(int _x, int _y, int _width, int _height)
  {
    rectangle = new Microsoft.Xna.Framework.Rectangle(_x, _y, _width, _height);
    _pos = new Vector2(_x, _y);
  }

  public Rectangle(Sprite _sprite)
  {
    rectangle = new Microsoft.Xna.Framework.Rectangle(_sprite.Shape.Position.ToPoint(), _sprite.Shape.Size);
  }

  public override bool IntersectsWith(Shape _shape)
  {
    return _shape.Intersects(this);
  }

  public override bool Intersects(Rectangle _rectangle)
  {
    return rectangle.Intersects(_rectangle.rectangle);
  }

  public override bool Intersects(Circle _circle)
  {
    return _circle.Intersects(this);
  }

  public Microsoft.Xna.Framework.Rectangle ToXNARectangle() => rectangle;

}