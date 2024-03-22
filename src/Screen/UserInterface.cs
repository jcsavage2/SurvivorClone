using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivorClone
{
  public class UserInterface
  {
    private SpriteFont font;

    private ProgressBar healthBar;

    public UserInterface() { }

    public void LoadContent(string _fontPath = "Font/File")
    {
      font = Globals.Content.Load<SpriteFont>(_fontPath);
      healthBar = new ProgressBar(font, Color.Red, Color.Green, new Vector2(15, 10), new Vector2(225, 20));
    }

    public void Update(Player player)
    {
      float percentRemainingHealth = player.Health / Player.MAX_HEALTH;
      healthBar.UpdateProgress(percentRemainingHealth);
    }

    public void Draw(Player player)
    {
      healthBar.Draw();
    }
  }
}
