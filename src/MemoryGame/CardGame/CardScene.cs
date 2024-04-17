using MemoryGame.CardGame.States;
using MemoryGame.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace MemoryGame.CardGame;

public sealed class CardScene(
    ContentManager contentManager,
    SpriteBatch spriteBatch,
    InputManager inputManager,
    IServiceProvider services
) : Scene
{
    private Texture2D _spritesheet = default!;
    private CardSceneState _state = default!;
    private Board _board = new(2, 3);

    public Card? FirstCard { get; set; }
    public Card? SecondCard { get; set; }

    public bool AllCardsCollected { get => _board.Cards.All(c => !c.IsVisible); }

    public override void LoadContent()
    {
        _spritesheet = contentManager.Load<Texture2D>("Cards/spritesheet");
        _board.Position = (spriteBatch.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2() - _board.Size) / 2;
        _state = services.GetRequiredService<FlipFirstCardState>();
    }

    public override void UnloadContent() => _spritesheet.Dispose();

    public void SetState<TState>() where TState : CardSceneState
    {
        _state = services.GetRequiredService<TState>();
    }

    public Card? GetClickedCard()
    {
        if (!inputManager.MouseLeftClicked) return null;
        foreach (var card in _board.Cards)
            if (card.IsVisible && card.DstRect.Intersects(inputManager.MouseRect))
                return card;
        return null;
    }

    public void RestartScene()
    {
        _board = _board.CreateNew((spriteBatch.GraphicsDevice.PresentationParameters.Bounds.Size.ToVector2() - _board.Size) / 2);
        _state = services.GetRequiredService<FlipFirstCardState>();
    }

    public override void Update(GameTime gameTime)
    {
        inputManager.Update(gameTime);
        if (inputManager.MouseRightClicked)
        {
            RestartScene();
            return;
        }
        _state.Update(gameTime);
    }

    public void DrawBoard()
    {
        foreach (var card in _board.Cards)
            if (card.IsVisible)
                spriteBatch.Draw(_spritesheet, card.Position, card.SrcRect, Color.White);
    }

    public override void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();
        _state.Draw(gameTime);
        spriteBatch.End();
    }
}