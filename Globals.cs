using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public static class Globals
{
  public static Point WindowSize { get; set; }
  public static ContentManager Content { get; set; }
  public static GraphicsDeviceManager Graphics { get; set; }
  public static SpriteBatch SpriteBatch { get; set; }

  public enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }

  public const int TILE_SIZE = 32;

  public static void Initialize(ContentManager content, GraphicsDeviceManager graphicsManager)
  {
    Content = content;
    Graphics = graphicsManager;

    WindowSize = new Point(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

    Content.RootDirectory = "Content";
  }

  public static void LoadContent(SpriteBatch spriteBatch)
  {
    SpriteBatch = spriteBatch;
  }
}
