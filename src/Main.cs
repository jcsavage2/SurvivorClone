using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public class Main : Game
{
  private readonly Player player;
  private readonly List<Enemy> enemies;
  private SpriteFont font;
  private readonly Map map;
  private readonly Camera camera;
  private readonly UserInterface userInterface;

  public Main()
  {
    Globals.Initialize(Content, new GraphicsDeviceManager(this));

    // Load user view
    map = new Map(50);
    camera = new Camera();
    userInterface = new UserInterface();

    // Load entities
    player = new Player(new Vector2(0, 0));
    enemies = new List<Enemy>
    {
      new Enemy(new Vector2(Globals.WindowSize.X / 3, Globals.WindowSize.Y / 3)),
      new Enemy(new Vector2(Globals.WindowSize.X / 4, Globals.WindowSize.Y / 4))
    };
  }

  protected override void Initialize()
  {
    Globals.Graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
    Globals.Graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
    Globals.Graphics.ApplyChanges();

    base.Initialize();
  }

  protected override void LoadContent()
  {
    Globals.LoadContent(new SpriteBatch(GraphicsDevice));

    map.LoadContent();

    //font = Content.Load<SpriteFont>("Font/File");
    player.LoadContent("yellow_character_small");
    player.SetBounds(map.mapDimensionsPixels);
    userInterface.LoadContent("Font/File");

    foreach (var enemy in enemies)
    {
      enemy.LoadContent("enemy");
    }
  }

  protected override void Update(GameTime gameTime)
  {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    player.Update(gameTime, enemies);
    camera.Follow(player, map);
    userInterface.Update(player);

    foreach (var enemy in enemies)
    {
      enemy.Update(gameTime);
    }

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.Gray);

    Globals.SpriteBatch.Begin(transformMatrix: camera.translation);
    map.Draw();
    player.Draw();
    foreach (var enemy in enemies)
    {
      enemy.Draw();
    }
    Globals.SpriteBatch.End();

    // Draw without camera translation
    Globals.SpriteBatch.Begin();
    userInterface.Draw(player);
    Globals.SpriteBatch.End();

    base.Draw(gameTime);
  }
}
