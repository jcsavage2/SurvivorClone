using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class RenderManager
{
  public float RatioX { get; set; }
  public float RatioY { get; set; }
  public SpriteFont Font { get; set; }
  public Point RenderSize { get; set; }

  private Point windowSize { get; set; }
  private Rectangle destinationRectangle;
  private RenderTarget2D renderTarget { get; set; }
  public ContentManager Content { get; set; }
  private GraphicsDeviceManager graphics { get; set; }
  private GameWindow window { get; set; }
  private SpriteBatch spriteBatch { get; set; }
  private Texture2D redTexture;

  public RenderManager(ContentManager _content, GraphicsDeviceManager _graphicsManager, Point _renderSize, GameWindow _window)
  {
    Content = _content;
    Content.RootDirectory = "Content";
    graphics = _graphicsManager;
    windowSize = _renderSize;
    RenderSize = _renderSize;
    window = _window;
  }

  public void LoadContent(string _fontPath = "Font/File")
  {
    spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
    renderTarget = new RenderTarget2D(graphics.GraphicsDevice, RenderSize.X, RenderSize.Y);
    Font = Content.Load<SpriteFont>(_fontPath);

    // Debug helper data
    redTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
    redTexture.SetData(new[] { Color.Red });

    changeWindowSize(windowSize);
  }

  private void changeWindowSize(Point size)
  {
    windowSize = size;
    graphics.PreferredBackBufferWidth = size.X;
    graphics.PreferredBackBufferHeight = size.Y;
    graphics.ApplyChanges();

    RatioX = (float)windowSize.X / RenderSize.X;
    RatioY = (float)windowSize.Y / RenderSize.Y;
    float scale = Math.Min(RatioX, RatioY);

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

  public void DrawScene(Camera _camera, Action _drawWithTranslation, Action _drawWithoutTranslation)
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
    spriteBatch.Draw(renderTarget, destinationRectangle.ToXNARectangle(), Color.White);
    spriteBatch.End();
  }

  public void DrawTexture(Texture2D _texture, Vector2 _position, Rectangle _rectangle)
  {
    spriteBatch.Draw(_texture, _position, _rectangle.ToXNARectangle(), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);

    if (Debug.IsActive())
    {
      DrawBorder(_position, _rectangle);
    }
  }

  public void DrawString(string _text, Vector2 _position, Color _color)
  {
    spriteBatch.DrawString(Font, _text, _position, _color);
  }

  private void DrawBorder(Vector2 _position, Rectangle _rectangle)
  {
    spriteBatch.Draw(redTexture, new Rectangle((int)_position.X, (int)_position.Y, _rectangle.Width, 1).ToXNARectangle(), Color.White);
    spriteBatch.Draw(redTexture, new Rectangle((int)_position.X, (int)_position.Y, 1, _rectangle.Height).ToXNARectangle(), Color.White);
    spriteBatch.Draw(redTexture, new Rectangle((int)_position.X + _rectangle.Width, (int)_position.Y, 1, _rectangle.Height).ToXNARectangle(), Color.White);
    spriteBatch.Draw(redTexture, new Rectangle((int)_position.X, (int)_position.Y + _rectangle.Height, _rectangle.Width, 1).ToXNARectangle(), Color.White);
  }
}
