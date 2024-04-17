using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame.States;

public sealed class FlipSecondCardState(CardScene scene) : PlayCardState(scene)
{
    public override void Update(GameTime gameTime)
    {
        var card = Scene.GetClickedCard();
        if (card is not null && card != Scene.FirstCard)
        {
            card.IsFlipped = true;
            Scene.SecondCard = card;
            Scene.SetState<ResolveTurnState>();
        }
    }
}
