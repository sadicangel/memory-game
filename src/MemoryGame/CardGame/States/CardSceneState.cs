using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame.States;

public abstract class CardSceneState
{
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime);
}
