using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MemoryGame.Common;
public sealed class InputManager(GraphicsDeviceManager graphicsDeviceManager)
{
    private MouseState _lastMouseState;

    public bool MouseLeftClicked { get; private set; }

    public bool MouseRightClicked { get; private set; }

    public Rectangle MouseRect { get; private set; }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        var withinWindow = mouseState.X >= 0
            && mouseState.Y >= 0
            && mouseState.X < graphicsDeviceManager.PreferredBackBufferWidth
            && mouseState.Y < graphicsDeviceManager.PreferredBackBufferHeight;

        MouseLeftClicked = withinWindow
            && mouseState.LeftButton == ButtonState.Pressed
            && _lastMouseState.LeftButton == ButtonState.Released;

        MouseRightClicked = withinWindow
            && mouseState.RightButton == ButtonState.Pressed
            && _lastMouseState.LeftButton == ButtonState.Released;

        MouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

        _lastMouseState = mouseState;

    }
}
