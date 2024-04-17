using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame.States;

public abstract class PlayCardState(CardScene scene) : CardSceneState
{
    protected CardScene Scene { get; } = scene;

    public override void Draw(GameTime gameTime) => Scene.DrawBoard();
}
