using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame;

public abstract class CardSceneState(CardScene scene)
{
    protected CardScene Scene { get; } = scene;

    public abstract void Update(GameTime gameTime);
}

public sealed class FlipFirstCardState(CardScene scene) : CardSceneState(scene)
{
    public override void Update(GameTime gameTime)
    {
        var card = Scene.GetClickedCard();
        if (card is not null)
        {
            card.IsFlipped = true;
            Scene.FirstCard = card;
            Scene.State = new FlipSecondCardState(Scene);
        }
    }
}

public sealed class FlipSecondCardState(CardScene scene) : CardSceneState(scene)
{
    public override void Update(GameTime gameTime)
    {
        var card = Scene.GetClickedCard();
        if (card is not null && card != Scene.FirstCard)
        {
            card.IsFlipped = true;
            Scene.SecondCard = card;
            Scene.State = new ResolveTurnState(Scene);
        }
    }
}

public sealed class ResolveTurnState(CardScene scene) : CardSceneState(scene)
{
    public override void Update(GameTime gameTime)
    {
        if (Scene.MouseClicked)
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
            Scene.State = new FlipFirstCardState(Scene);
        }
    }
}
