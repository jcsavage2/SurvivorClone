using System;
using System.Collections.Generic;
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

  public AnimatedSprite(Vector2 position, int _totalStates, int _totalFrames, Point _tileSize, float _frameDelay = .1f)
    : base(position)
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

  public override void LoadContent(string texturePath)
  {
    spriteTexture = RenderManager.Content.Load<Texture2D>(texturePath);
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

  private void resetAnimation()
  {
    currentFrame = 0;
    frameDelayCounter = 0;
  }

  public override void SetBounds(Point mapSizePixels)
  {
    minPos = new Vector2(ORIGIN_OFFSET, ORIGIN_OFFSET);
    maxPos = new Vector2(mapSizePixels.X - tileSize.X, mapSizePixels.Y - tileSize.Y);
  }

  // Sprite animated tilesheets have a pixel border and padding between tiles
  // Alongside this we have to offset the origin rectangle we want to draw from the tile sheet
  private int getTilePosition(int index, int tileSize)
  {
    return PADDING * (index * 2 + 2) + index * tileSize;
  }

  public override void Draw()
  {
    RenderManager.SpriteBatch.Draw(
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
}
