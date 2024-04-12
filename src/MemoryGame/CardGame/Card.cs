using Microsoft.Xna.Framework;

namespace MemoryGame.CardGame;

public sealed class Card
{
    public static readonly Point Size = new(71, 96);

    public required Suit Suit { get; set; }
    public required Rank Rank { get; set; }

    public required Vector2 Position { get; set; }

    public bool IsFlipped { get; set; }

    public Rectangle SrcRect { get => new((IsFlipped ? new Point((int)Rank, (int)Suit) : new Point(1, 4)) * Size, Size); }

    public Rectangle DstRect { get => new(Position.ToPoint(), Size); }
}

public enum Suit
{
    Spades,
    Hearts,
    Clubs,
    Diamonds,
}

public enum Rank
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