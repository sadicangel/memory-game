using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MemoryGame.CardGame;

public sealed class CardScene : Scene
{
    private Texture2D _spritesheet;
    private MouseState _lastMouseState;

    public bool MouseClicked { get; private set; }

    public Board Board { get; }
    public Card FirstCard { get; set; }
    public Card SecondCard { get; set; }
    public GameState State { get; set; }

    public CardScene(SceneManager sceneManager) : base(sceneManager)
    {
        Board = new Board(4, 4);
        State = new FlipFirstCardState(this);
    }

    public override void LoadContent(ContentManager contentManager)
    {
        _spritesheet = contentManager.Load<Texture2D>("Cards/spritesheet");
    }

    public override void UnloadContent() => _spritesheet.Dispose();

    public Card GetClickedCard()
    {
        if (!MouseClicked) return null;
        var mouseRect = new Rectangle(_lastMouseState.Position, new Point(1, 1));
        foreach (var card in Board.Cards)
            if (card.IsVisible && card.DstRect.Intersects(mouseRect))
                return card;
        return null;
    }

    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        MouseClicked = mouseState.LeftButton == ButtonState.Pressed
            && _lastMouseState.LeftButton == ButtonState.Released;
        _lastMouseState = mouseState;

        State.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        foreach (var card in Board.Cards)
        {
            if (card.IsVisible)
            {
                spriteBatch.Draw(_spritesheet, card.Position, card.SrcRect, Color.White);
            }
        }
        spriteBatch.End();
    }

}