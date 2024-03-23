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
    spriteTexture = Globals.Content.Load<Texture2D>(texturePath);
    center = new Vector2(tileSize.X / 2, tileSize.Y / 2);
    rectangle = new Rectangle(0, 0, tileSize.X, tileSize.Y);
  }

  public void Update(GameTime gameTime)
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

  public override void Draw()
  {
    Globals.SpriteBatch.Draw(
      spriteTexture,
      position,
      new Rectangle(
        (5 * (currentFrame * 2 + 2)) + currentFrame * tileSize.X,
        (5 * (currentState * 2 + 2)) + currentState * tileSize.Y,
        tileSize.X,
        tileSize.Y
      ),
      Color.White,
      0,
      center,
      Vector2.One,
      SpriteEffects.None,
      1
    );
  }
}
