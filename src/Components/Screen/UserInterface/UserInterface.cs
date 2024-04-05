using Microsoft.Xna.Framework;

namespace SurvivorClone
{
  public class UserInterface
  {
    private ProgressBar healthBar;
    private Timer timer;

    public UserInterface() { }

    public void LoadContent(RenderManager _renderManager)
    {
      healthBar = new ProgressBar(_renderManager, new Vector2(0, 0), .01f, .01f);
      timer = new Timer(_renderManager, new Vector2(_renderManager.GetRenderSize().X, 0), .01f, -.2f);
    }

    public void Update(RenderManager _renderManager, GameTime gameTime, Player player)
    {
      // Update positions from render target window size
      float percentRemainingHealth = player.GetHealth() / Player.MAX_HEALTH;
      healthBar.Update(_renderManager, percentRemainingHealth);
      timer.Update(_renderManager, gameTime);
    }

    public void Draw(RenderManager _renderManager, Player player)
    {
      healthBar.Draw(_renderManager);
      timer.Draw(_renderManager);
    }
  }
}
