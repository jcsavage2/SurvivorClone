using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone
{
  public class UserInterface
  {
    private SpriteFont font;

    private ProgressBar healthBar;
    private Timer timer;

    public UserInterface() { }

    public void LoadContent(string _fontPath = "Font/File")
    {
      font = Globals.Content.Load<SpriteFont>(_fontPath);
      healthBar = new ProgressBar(font, new Vector2(160, 20));
      timer = new Timer(new Vector2(Globals.WindowSize.X - 75, 15), font);
      timer.LoadContent("UI/timer_background");
    }

    public void Update(GameTime gameTime, Player player)
    {
      float percentRemainingHealth = player.Health / Player.MAX_HEALTH;
      healthBar.UpdateProgress(percentRemainingHealth);
      timer.Update(gameTime);
    }

    public void Draw(Player player)
    {
      healthBar.Draw();
      timer.Draw();
    }
  }
}
