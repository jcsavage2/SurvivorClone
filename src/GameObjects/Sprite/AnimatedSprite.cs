using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone;

public class AnimatedSprite : Sprite
{
  public int CurrentState { get; set; }
  public bool IsActive { get; set; }
  public int CurrentFrame { get; set; }

  protected readonly int totalStates;

  protected readonly int totalFrames;
  protected readonly float frameDelay;
  protected float frameDelayCounter;

  private const int PADDING = 1;

  public AnimatedSprite(
    RenderManager _renderManager,
    string _texturePath,
    Vector2 _position,
    Geometry.CollisionTypes _collisionType,
    int _totalStates,
    int _totalFrames,
    int _initialState,
    Point _size,
    float _frameDelay = .1f
  )
    : base(_renderManager, _texturePath, _position, _collisionType)
  {
    CurrentState = _initialState;
    CurrentFrame = 0;
    totalStates = _totalStates;
    totalFrames = _totalFrames;
    Shape = Shape.CreateShape(_position, _size, _collisionType);
    frameDelay = _frameDelay;
    frameDelayCounter = 0;
    IsActive = true;
    spriteTexture = _renderManager.Content.Load<Texture2D>(_texturePath);
  }

  public virtual void Update(GameTime gameTime)
  {
    if (!IsActive)
    {
      return;
    }

    frameDelayCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

    if (frameDelayCounter >= frameDelay)
    {
      frameDelayCounter = 0;
      CurrentFrame++;

      if (CurrentFrame >= totalFrames)
      {
        CurrentFrame = 0;
      }
    }
  }

  public void StartAnimation()
  {
    IsActive = true;
  }

  public void StopAnimation()
  {
    IsActive = false;
  }

  public override void Draw(RenderManager _renderManager)
  {
    _renderManager.DrawTexture(
      spriteTexture,
      Shape.Position,
      new Rectangle(getTilePosition(CurrentFrame, Shape.Width), getTilePosition(CurrentState, Shape.Height), Shape.Width, Shape.Height)
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
    CurrentFrame = 0;
    frameDelayCounter = 0;
  }

  // --- SET --- //
  public void SetState(int newState)
  {
    CurrentState = Math.Clamp(newState, 0, totalStates - 1);
    resetAnimation();
  }
}
