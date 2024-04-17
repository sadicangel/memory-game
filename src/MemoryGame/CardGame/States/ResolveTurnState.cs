using MemoryGame.Common;
using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame.States;

public sealed class ResolveTurnState(CardScene scene, InputManager inputManager) : PlayCardState(scene)
{
    public override void Update(GameTime gameTime)
    {
        if (inputManager.MouseLeftClicked)
        {
            if (Scene.FirstCard!.Id == Scene.SecondCard!.Id)
            {
                Scene.FirstCard.IsVisible = false;
                Scene.SecondCard.IsVisible = false;
            }
            else
            {

                Scene.FirstCard.IsFlipped = false;
                Scene.SecondCard.IsFlipped = false;
            }

            Scene.FirstCard = null;
            Scene.SecondCard = null;
            if (Scene.AllCardsCollected)
            {
                Scene.SetState<WinCardGameState>();
            }
            else
            {
                Scene.SetState<FlipFirstCardState>();
            }
        }
    }
}
