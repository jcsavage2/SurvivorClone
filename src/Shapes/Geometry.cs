using System;
using Microsoft.Xna.Framework;

namespace SurvivorClone;

public static class Geometry
{
  public enum CollisionTypes
  {
    RECTANGLE,
    CIRCLE
  }

  public static float EuclideanDistance(float x1, float y1, float x2, float y2)
  {
    return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
  }

  public static float EuclideanDistance(Vector2 _point1, Vector2 _point2)
  {
    return EuclideanDistance(_point1.X, _point1.Y, _point2.X, _point2.Y);
  }
}