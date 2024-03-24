using Microsoft.Xna.Framework;

namespace SurvivorClone
{
  public class UserInterface
  {
    private ProgressBar healthBar;
    private Timer timer;

    public UserInterface() { }

    public void LoadContent()
    {
      healthBar = new ProgressBar(new Vector2(0, 0), .01f, .01f);
      timer = new Timer(new Vector2(RenderManager.RenderTarget.Width, 0), .01f, -.2f);
    }

    public void Update(GameTime gameTime, Player player)
    {
      // Update positions from render target window size
      float percentRemainingHealth = player.Health / Player.MAX_HEALTH;
      healthBar.Update(percentRemainingHealth);
      timer.Update(gameTime);
    }

    public void Draw(Player player)
    {
      healthBar.Draw();
      timer.Draw();
    }
  }
}
