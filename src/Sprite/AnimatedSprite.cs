using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class AnimatedSprite : Sprite
{
  // State management
  private int currentState;
  private readonly int totalStates;

  // Animation management
  private bool isActive;
  private readonly Point tileSize;
  private int currentFrame;
  private readonly int totalFrames;
  private readonly float frameDelay;
  private float frameDelayCounter;

  private const int PADDING = 1;

  public AnimatedSprite(RenderManager _renderManager, Vector2 _position, int _totalStates, int _totalFrames, Point _tileSize, float _frameDelay = .1f)
    : base(_renderManager, _position)
  {
    currentState = 0;
    currentFrame = 0;
    totalStates = _totalStates;
    totalFrames = _totalFrames;
    tileSize = _tileSize;
    frameDelay = _frameDelay;
    frameDelayCounter = 0;
    isActive = true;
  }

  public override void LoadContent(RenderManager _renderManager, string _texturePath)
  {
    spriteTexture = _renderManager.GetContent().Load<Texture2D>(_texturePath);
    center = new Vector2(tileSize.X / 2, tileSize.Y / 2);
    rectangle = new Rectangle(0, 0, tileSize.X, tileSize.Y);
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!isActive)
    {
      return;
    }

    frameDelayCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

    if (frameDelayCounter >= frameDelay)
    {
      frameDelayCounter = 0;
      currentFrame++;

      if (currentFrame >= totalFrames)
      {
        currentFrame = 0;
      }
    }
  }

  public void StartAnimation()
  {
    isActive = true;
  }

  public void StopAnimation()
  {
    isActive = false;
  }

  public void ChangeState(int newState)
  {
    currentState = Math.Clamp(newState, 0, totalStates - 1);
    resetAnimation();
  }

  public void DrawWithScale(RenderManager _renderManager)
  {
    var drawPosition = position * new Vector2(_renderManager.GetRatioX(), _renderManager.GetRatioY());
    _renderManager
      .GetSpriteBatch()
      .Draw(
        spriteTexture,
        drawPosition,
        new Rectangle(getTilePosition(currentFrame, tileSize.X), getTilePosition(currentState, tileSize.Y), tileSize.X, tileSize.Y),
        Color.White,
        0,
        center,
        Vector2.One,
        SpriteEffects.None,
        1
      );
  }

  public override void Draw(RenderManager _renderManager)
  {
    _renderManager
      .GetSpriteBatch()
      .Draw(
        spriteTexture,
        position,
        new Rectangle(getTilePosition(currentFrame, tileSize.X), getTilePosition(currentState, tileSize.Y), tileSize.X, tileSize.Y),
        Color.White,
        0,
        center,
        Vector2.One,
        SpriteEffects.None,
        1
      );
  }

  // Sprite animated tilesheets have a pixel border and padding between tiles
  // Alongside this we have to offset the origin rectangle we want to draw from the tile sheet
  private int getTilePosition(int _index, int _tileSize)
  {
    return PADDING * (_index * 2 + 2) + _index * _tileSize;
  }

  private void resetAnimation()
  {
    currentFrame = 0;
    frameDelayCounter = 0;
  }

  // Getters

  public int GetCurrentState() => currentState;

  public int GetCurrentFrame() => currentFrame;

  public bool IsActive() => isActive;
}
