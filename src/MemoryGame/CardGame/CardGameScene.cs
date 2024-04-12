using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MemoryGame.CardGame;
public sealed class CardGameScene(SceneManager sceneManager) : Scene(sceneManager)
{
    private Texture2D _spritesheet;
    private KeyboardState _prevKbdState;
    private Board _board = new Board(3, 6);

    public override void LoadContent(ContentManager contentManager)
    {
        _spritesheet = contentManager.Load<Texture2D>("Cards/spritesheet");
    }

    public override void UnloadContent() => _spritesheet.Dispose();

    public override void Update(GameTime gameTime)
    {
        var kbdState = Keyboard.GetState();
        //var suit = _card.Suit;
        //var rank = _card.Rank;
        //if (kbdState.IsKeyDown(Keys.Space) && !_prevKbdState.IsKeyDown(Keys.Space))
        //{
        //    rank++;
        //    if (rank > CardRank.King)
        //    {
        //        rank = 0;
        //        suit++;
        //        if (suit > CardSuit.Diamonds)
        //            suit = 0;
        //    }
        //    _card = new Card { Suit = suit, Rank = rank };
        //}
        _prevKbdState = kbdState;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        foreach (var card in _board.Cards)
        {
            spriteBatch.Draw(_spritesheet, card.Position, card.Rect, Color.White);
        }
        spriteBatch.End();
    }

}