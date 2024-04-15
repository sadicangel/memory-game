using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace MemoryGame.CardGame;
public sealed class Board(int rows, int cols, int spacing = 6)
{
    private Vector2 _position;

    public Card[] Cards { get; } = GetCards(rows, cols, spacing);

    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            UpdatePositions(value);
        }
    }

    public Vector2 Size { get => Cards[^1].Position + Card.Size.ToVector2() - Cards[0].Position; }

    private void UpdatePositions(Vector2 boardXy)
    {
        var count = rows * cols / 2;
        for (int i = 0, j = count; i < count; ++i, ++j)
        {
            Cards[i].Position = boardXy + (new Point(i % cols, i / cols) * (new Point(spacing) + Card.Size)).ToVector2();
            Cards[j].Position = boardXy + (new Point(j % cols, j / cols) * (new Point(spacing) + Card.Size)).ToVector2();
        }
    }

    private static Card[] GetCards(int rows, int cols, int spacing)
    {
        var count = rows * cols / 2;
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, 52);

        var pool = Enumerable
            .Range((int)Suit.Spades, (int)Suit.Diamonds)
            .SelectMany(suit => Enumerable.Range((int)Rank.Ace, (int)Rank.King)
                .Select(rank => (Suit: (Suit)suit, Rank: (Rank)rank)))
            .ToArray();

        Random.Shared.Shuffle(pool);

        var cards = new Card[count * 2];

        for (int i = 0, j = count; i < count; ++i, ++j)
        {
            cards[i] = new Card
            {
                Suit = pool[i].Suit,
                Rank = pool[i].Rank,
                Position = default
            };
            cards[j] = new Card
            {
                Suit = pool[i].Suit,
                Rank = pool[i].Rank,
                Position = default
            };
        }

        Random.Shared.Shuffle(cards);

        for (int i = 0, j = count; i < count; ++i, ++j)
        {
            cards[i].Position = (new Point(i % cols, i / cols) * (new Point(spacing) + Card.Size)).ToVector2();
            cards[j].Position = (new Point(j % cols, j / cols) * (new Point(spacing) + Card.Size)).ToVector2();
        }

        return cards;
    }
}
