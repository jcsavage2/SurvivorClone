using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class GameManager : Game
{
  private readonly Player player;
  private readonly Map map;
  private readonly Camera camera;
  private readonly UserInterface userInterface;

  public static readonly LogManager Logger = new LogManager();

  public GameManager(bool debug = false)
  {
    Logger.InitLog(debug);
    try
    {
      RenderManager.Create(Content, new GraphicsDeviceManager(this), new Point(960, 540), Window);

      // Load user view
      map = new Map(50, 32);
      camera = new Camera();
      userInterface = new UserInterface();

      // Load entities
      player = new Player(new Vector2(200, 200), 2, 11, new Point(64, 64));
    }
    catch (Exception ex)
    {
      Logger.HandleError(ex, "Error in GameManager ctor");
    }
  }

  protected override void LoadContent()
  {
    try
    {
      RenderManager.LoadContent(new SpriteBatch(GraphicsDevice));
      userInterface.LoadContent();
      map.LoadContent();

      player.LoadContent("Sprites/player");
      player.SetBounds(map.mapDimensionsPixels);
    }
    catch (Exception ex)
    {
      Logger.HandleError(ex, "Error in LoadContent");
    }
  }

  protected override void Update(GameTime gameTime)
  {
    try
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      RenderManager.Update();

      player.Update(gameTime);
      camera.Update(player, map);
      userInterface.Update(gameTime, player);

      base.Update(gameTime);
    }
    catch (Exception ex)
    {
      Logger.HandleError(ex, "Error in Update");
    }
  }

  protected override void Draw(GameTime gameTime)
  {
    try
    {
      RenderManager.Activate();

      RenderManager.SpriteBatch.Begin(transformMatrix: camera.translation);
      map.Draw();
      player.Draw();
      RenderManager.SpriteBatch.End();

      // Draw UI without camera translation
      RenderManager.SpriteBatch.Begin();
      userInterface.Draw(player);
      RenderManager.SpriteBatch.End();

      RenderManager.Draw();

      base.Draw(gameTime);
    }
    catch (Exception ex)
    {
      Logger.HandleError(ex, "Error in Draw");
    }
  }
}
