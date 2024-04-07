using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class GameManager : Game
{
  private readonly RenderManager renderManager;
  private readonly EnemyManager enemyManager;

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
      renderManager = new RenderManager(Content, new GraphicsDeviceManager(this), new Point(960, 540), Window);
      enemyManager = new EnemyManager(10, 2, "Sprites/enemy");

      // Load user view
      map = new Map(30, 64);
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
      renderManager.LoadContent(new SpriteBatch(GraphicsDevice));
      userInterface.LoadContent(renderManager);
      map.LoadContent(renderManager);

      player.LoadContent(renderManager, "Sprites/player", map);
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
      renderManager.UpdateWindowSize();

      player.Update(gameTime, map);
      camera.Update(renderManager, player, map);
      userInterface.Update(renderManager, gameTime, player);

      enemyManager.Update(renderManager, gameTime, map, player);

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
      // First callback draws using camera translation
      // Second callback draws without camera translation
      renderManager.Draw(
        camera,
        () =>
        {
          map.Draw(renderManager);
          player.DrawWithScale(renderManager);
          enemyManager.Draw(renderManager);
        },
        () =>
        {
          userInterface.Draw(renderManager, player);
        }
      );

      base.Draw(gameTime);
    }
    catch (Exception ex)
    {
      Logger.HandleError(ex, "Error in Draw");
    }
  }
}
