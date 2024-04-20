using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class GameManager : Game
{
  private readonly RenderManager renderManager;
  private EnemyManager enemyManager;

  private Player player;
  private Map map;
  private Camera camera;

  private UserInterface userInterface;

  public GameManager(bool debug = false)
  {
    try
    {
      Debug.Init(debug);

      renderManager = new RenderManager(Content, new GraphicsDeviceManager(this), new Point(960, 540), Window);
    }
    catch (Exception ex)
    {
      Debug.HandleError(ex);
    }
  }

  protected override void LoadContent()
  {
    try
    {
      renderManager.LoadContent();

      userInterface = new UserInterface(renderManager);

      camera = new Camera();
      map = new Map(renderManager, 30, 64);

      // Load entities
      player = new Player(renderManager, "Sprites/player", new Vector2(200, 200), 2, 11, new Point(64, 64));
      enemyManager = new EnemyManager(0, 2, "Sprites/enemy");
    }
    catch (Exception ex)
    {
      Debug.HandleError(ex);
    }
  }

  protected override void Update(GameTime gameTime)
  {
    try
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      if (InputManager.IsKeyPressed(Keys.F4))
        Debug.SetActive(!Debug.IsActive());

      renderManager.UpdateWindowSize();

      player.Update(renderManager, gameTime, map);
      camera.Update(renderManager, player, map);
      userInterface.Update(renderManager, gameTime, player);

      enemyManager.Update(renderManager, gameTime, map, player);

      base.Update(gameTime);
    }
    catch (Exception ex)
    {
      Debug.HandleError(ex);
    }
  }

  protected override void Draw(GameTime gameTime)
  {
    try
    {
      // First callback draws using camera translation
      // Second callback draws without camera translation
      renderManager.DrawScene(
        camera,
        () =>
        {
          map.Draw(renderManager);
          player.Draw(renderManager);
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
      Debug.HandleError(ex);
    }
  }
}
