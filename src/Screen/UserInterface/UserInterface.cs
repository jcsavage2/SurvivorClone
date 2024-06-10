using Microsoft.Xna.Framework;

namespace SurvivorClone
{
  public class UserInterface
  {
    private readonly ProgressBar healthBar;
    private readonly Timer timer;

    public UserInterface(RenderManager _renderManager)
    {
      healthBar = new ProgressBar(_renderManager, new Vector2(0, 0), 5, 5);
      timer = new Timer(_renderManager, new Vector2(_renderManager.RenderSize.X, 0), 5, -160);
    }

    public void Update(RenderManager _renderManager, GameTime gameTime, Player player)
    {
      float percentRemainingHealth = player.Health / Player.MAX_HEALTH;
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
