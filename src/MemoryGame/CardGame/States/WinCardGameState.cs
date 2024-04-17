using MemoryGame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MemoryGame.CardGame.States;

public sealed class WinCardGameState(CardScene scene, ResourceCache resourceCache, InputManager inputManager, SpriteBatch spriteBatch) : CardSceneState
{
    private const string DisplayText = "You win!";

    private readonly SpriteFont _font = resourceCache.GetResource<SpriteFont>("GameFont");
    private readonly Vector2 _position = GetPosition(
        spriteBatch.GraphicsDevice.PresentationParameters.Bounds,
        resourceCache.GetResource<SpriteFont>("GameFont"),
        DisplayText);

    private static Vector2 GetPosition(Rectangle window, SpriteFont font, string text)
    {
        var fontSize = font.MeasureString(text);
        return new Vector2(window.Width - fontSize.X, window.Height - fontSize.Y) / 2;
    }

    public override void Update(GameTime gameTime)
    {
        if (inputManager.MouseLeftClicked)
        {
            scene.RestartScene();
        }
        var q = _font.MeasureString("You win!");

    }

    public override void Draw(GameTime gameTime)
    {
        spriteBatch.DrawString(_font, DisplayText, _position, Color.Black);
    }
}
