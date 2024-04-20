using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class AnimatedSprite : Sprite
{
  // State management
  protected int currentState;
  protected readonly int totalStates;

  // Animation management
  protected bool isActive;
  protected int currentFrame;
  protected readonly int totalFrames;
  protected readonly float frameDelay;
  protected float frameDelayCounter;

  private const int PADDING = 1;

  public AnimatedSprite(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    int _totalStates,
    int _totalFrames,
    Point _size,
    float _frameDelay = .1f
  )
    : base(_renderManager, _texturePath, _position)
  {
    currentState = 0;
    currentFrame = 0;
    totalStates = _totalStates;
    totalFrames = _totalFrames;
    size = _size;
    frameDelay = _frameDelay;
    frameDelayCounter = 0;
    isActive = true;
    spriteTexture = _renderManager.GetContent().Load<Texture2D>(_texturePath);
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

  public override void Draw(RenderManager _renderManager)
  {
    _renderManager.DrawTexture(
      spriteTexture,
      position,
      new Rectangle(getTilePosition(currentFrame, size.X), getTilePosition(currentState, size.Y), size.X, size.Y)
    );
  }

  // --- HELPERS --- //

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

  // --- SET --- //
  public void SetState(int newState)
  {
    currentState = Math.Clamp(newState, 0, totalStates - 1);
    resetAnimation();
  }

  // --- GET --- //

  public int GetCurrentState() => currentState;

  public int GetCurrentFrame() => currentFrame;

  public bool IsActive() => isActive;
}
