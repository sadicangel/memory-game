using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MemoryGame.CardGame;

public sealed class CardScene : Scene
{
    private Texture2D _spritesheet = default!;
    private MouseState _lastMouseState;
    private readonly ContentManager _contentManager;
    private readonly SpriteBatch _spriteBatch;

    public bool MouseClicked { get; private set; }

    public Board Board { get; }
    public Card? FirstCard { get; set; }
    public Card? SecondCard { get; set; }
    public CardSceneState State { get; set; }

    public CardScene(ContentManager contentManager, SpriteBatch spriteBatch)
    {
        Board = new Board(4, 4);
        State = new FlipFirstCardState(this);
        _contentManager = contentManager;
        _spriteBatch = spriteBatch;
    }

    public override void LoadContent()
    {
        _spritesheet = _contentManager.Load<Texture2D>("Cards/spritesheet");
        Board.Position = (_spriteBatch.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2() - Board.Size) / 2;
    }

    public override void UnloadContent() => _spritesheet.Dispose();

    public Card? GetClickedCard()
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

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        foreach (var card in Board.Cards)
        {
            if (card.IsVisible)
            {
                _spriteBatch.Draw(_spritesheet, card.Position, card.SrcRect, Color.White);
            }
        }
        _spriteBatch.End();
    }
}