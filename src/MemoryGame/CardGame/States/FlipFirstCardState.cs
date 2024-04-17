using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame.States;

public sealed class FlipFirstCardState(CardScene scene) : PlayCardState(scene)
{
    public override void Update(GameTime gameTime)
    {
        var card = Scene.GetClickedCard();
        if (card is not null)
        {
            card.IsFlipped = true;
            Scene.FirstCard = card;
            Scene.SetState<FlipSecondCardState>();
        }
    }
}
