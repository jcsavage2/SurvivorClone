using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SurvivorClone;

public static class InputManager
{
  public static KeyboardState CurrentKeyboardState { get; set; }
  public static KeyboardState PreviousKeyboardState { get; set; }
  public static MouseState CurrentMouseState { get; set; }
  public static MouseState PreviousMouseState { get; set; }

  public static void Update()
  {
    PreviousKeyboardState = CurrentKeyboardState;
    CurrentKeyboardState = Keyboard.GetState();

    PreviousMouseState = CurrentMouseState;
    CurrentMouseState = Mouse.GetState();
  }

  public static bool IsKeyPressed(Keys key)
  {
    return CurrentKeyboardState.IsKeyDown(key) && !PreviousKeyboardState.IsKeyDown(key);
  }

  public static Vector2 GetMousePosition()
  {
    return new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
  }
}
