using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public static class RenderManager
{
  // Screen properties for scaling rendering
  public static Point WindowSize { get; set; }
  public static Point RenderSize { get; set; }
  private static Rectangle destinationRectangle;

  // Screen management
  public static RenderTarget2D RenderTarget { get; set; }
  public static ContentManager Content { get; set; }
  public static GraphicsDeviceManager Graphics { get; set; }
  public static SpriteBatch SpriteBatch { get; set; }
  public static GameWindow Window { get; set; }

  // Fonts
  public static SpriteFont Font { get; set; }

  public static void Create(ContentManager content, GraphicsDeviceManager graphicsManager, Point renderSize, GameWindow window)
  {
    Content = content;
    Graphics = graphicsManager;

    WindowSize = renderSize;

    Content.RootDirectory = "Content";
    RenderSize = renderSize;
    Window = window;
  }

  public static void LoadContent(SpriteBatch spriteBatch, string _fontPath = "Font/File")
  {
    SpriteBatch = spriteBatch;
    RenderTarget = new RenderTarget2D(Graphics.GraphicsDevice, RenderSize.X, RenderSize.Y);
    Font = Content.Load<SpriteFont>(_fontPath);
    ChangeWindowSize(WindowSize);
  }

  public static void ChangeWindowSize(Point size)
  {
    WindowSize = size;
    Graphics.PreferredBackBufferWidth = size.X;
    Graphics.PreferredBackBufferHeight = size.Y;
    Graphics.ApplyChanges();
    SetDestinationRectangle();
  }

  public static void SetDestinationRectangle()
  {
    float scaleX = (float)WindowSize.X / RenderTarget.Width;
    float scaleY = (float)WindowSize.Y / RenderTarget.Height;
    float scale = Math.Min(scaleX, scaleY);

    int newWidth = (int)(RenderTarget.Width * scale);
    int newHeight = (int)(RenderTarget.Height * scale);

    int posX = (WindowSize.X - newWidth) / 2;
    int posY = (WindowSize.Y - newHeight) / 2;

    destinationRectangle = new Rectangle(posX, posY, newWidth, newHeight);
  }

  public static void Update()
  {
    InputManager.Update();
    if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.F1))
    {
      ChangeWindowSize(new Point(960, 540));
    }
    if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.F2))
    {
      ChangeWindowSize(new Point(1280, 720));
    }
    if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.F3))
    {
      ChangeWindowSize(new Point(1920, 1080));
    }
  }

  public static void Activate()
  {
    Graphics.GraphicsDevice.SetRenderTarget(RenderTarget);
    Graphics.GraphicsDevice.Clear(Color.Gray);
  }

  public static void Draw()
  {
    Graphics.GraphicsDevice.SetRenderTarget(null);
    Graphics.GraphicsDevice.Clear(Color.Gray);
    SpriteBatch.Begin();
    SpriteBatch.Draw(RenderTarget, destinationRectangle, Color.White);
    SpriteBatch.End();
  }
}
