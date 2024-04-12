using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MemoryGame.CardGame;
public sealed class CardScene(SceneManager sceneManager) : Scene(sceneManager)
{
    private Texture2D _spritesheet;
    private MouseState _prevMouseState;
    private Board _board = new Board(4, 8);

    public override void LoadContent(ContentManager contentManager)
    {
        _spritesheet = contentManager.Load<Texture2D>("Cards/spritesheet");
    }

    public override void UnloadContent() => _spritesheet.Dispose();

    private Card GetClickedCard(Rectangle mouseRect)
    {
        foreach (var card in _board.Cards)
            if (card.DstRect.Intersects(mouseRect))
                return card;
        return null;
    }

    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
        {
            var card = GetClickedCard(new Rectangle(mouseState.Position, new Point(1, 1)));
            if (card is not null)
                card.IsFlipped = !card.IsFlipped;
        }
        _prevMouseState = mouseState;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        foreach (var card in _board.Cards)
        {
            spriteBatch.Draw(_spritesheet, card.Position, card.SrcRect, Color.White);
        }
        spriteBatch.End();
    }

}