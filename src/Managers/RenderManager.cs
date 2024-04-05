using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class RenderManager
{
  // Screen properties for scaling rendering
  private Point windowSize { get; set; }
  private Point renderSize { get; set; }
  private Rectangle destinationRectangle;

  // Screen management
  private float ratioX;
  private float ratioY;
  private RenderTarget2D renderTarget { get; set; }
  private ContentManager content { get; set; }
  private GraphicsDeviceManager graphics { get; set; }
  private SpriteBatch spriteBatch { get; set; }
  private GameWindow window { get; set; }

  // Fonts
  private SpriteFont font { get; set; }

  public RenderManager(ContentManager _content, GraphicsDeviceManager _graphicsManager, Point _renderSize, GameWindow _window)
  {
    content = _content;
    graphics = _graphicsManager;

    windowSize = _renderSize;

    content.RootDirectory = "Content";
    renderSize = _renderSize;
    window = _window;
  }

  public void LoadContent(SpriteBatch _spriteBatch, string _fontPath = "Font/File")
  {
    spriteBatch = _spriteBatch;
    renderTarget = new RenderTarget2D(graphics.GraphicsDevice, renderSize.X, renderSize.Y);
    font = content.Load<SpriteFont>(_fontPath);
    changeWindowSize(windowSize);
  }

  private void changeWindowSize(Point size)
  {
    windowSize = size;
    graphics.PreferredBackBufferWidth = size.X;
    graphics.PreferredBackBufferHeight = size.Y;
    graphics.ApplyChanges();
    setDestinationRectangle();
  }

  private void setDestinationRectangle()
  {
    ratioX = (float)windowSize.X / renderSize.X;
    ratioY = (float)windowSize.Y / renderSize.Y;
    float scale = Math.Min(ratioX, ratioY);

    int newWidth = (int)(renderTarget.Width * scale);
    int newHeight = (int)(renderTarget.Height * scale);

    int posX = (windowSize.X - newWidth) / 2;
    int posY = (windowSize.Y - newHeight) / 2;

    destinationRectangle = new Rectangle(posX, posY, newWidth, newHeight);
  }

  public void UpdateWindowSize()
  {
    InputManager.Update();
    if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.F1))
    {
      changeWindowSize(new Point(960, 540));
    }
    if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.F2))
    {
      changeWindowSize(new Point(1280, 720));
    }
    if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.F3))
    {
      changeWindowSize(new Point(1920, 1080));
    }
  }

  public void Draw(Camera _camera, Action _drawWithTranslation, Action _drawWithoutTranslation)
  {
    graphics.GraphicsDevice.SetRenderTarget(renderTarget);
    graphics.GraphicsDevice.Clear(Color.Gray);

    // Draw with camera translation(offset) to render target
    spriteBatch.Begin(transformMatrix: _camera.translation);
    _drawWithTranslation();
    spriteBatch.End();

    // Draw UI without camera translation to render target
    spriteBatch.Begin();
    _drawWithoutTranslation();
    spriteBatch.End();

    // Draw the render target to the screen
    graphics.GraphicsDevice.SetRenderTarget(null);
    graphics.GraphicsDevice.Clear(Color.Gray);
    spriteBatch.Begin();
    spriteBatch.Draw(renderTarget, destinationRectangle, Color.White);
    spriteBatch.End();
  }

  // Getters
  public float GetRatioX() => ratioX;

  public float GetRatioY() => ratioY;

  public Point GetWindowSize() => windowSize;

  public Point GetRenderSize() => renderSize;

  public SpriteFont GetFont() => font;

  public GraphicsDeviceManager GetGraphics() => graphics;

  public GameWindow GetWindow() => window;

  public ContentManager GetContent() => content;

  public SpriteBatch GetSpriteBatch() => spriteBatch;
}
