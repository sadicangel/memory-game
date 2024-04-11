using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame;

public readonly record struct Card
{
    public static readonly Point Size = new(71, 96);

    public required CardSuit Suit { get; init; }
    public required CardRank Rank { get; init; }

    public Point SpriteSheetPosition { get => new Point((int)Rank, (int)Suit); }
}

public enum CardSuit
{
    Spades,
    Hearts,
    Clubs,
    Diamonds,
}

public enum CardRank
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
}