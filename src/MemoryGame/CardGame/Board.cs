using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace MemoryGame.CardGame;
public sealed class Board(int rows, int cols)
{
    public readonly Card[] Cards = GetCards(rows, cols);

    private static Card[] GetCards(int rows, int cols)
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
                Position = (new Point(i % cols, i / cols) * Card.Size).ToVector2(),
            };
            cards[j] = new Card
            {
                Suit = pool[i].Suit,
                Rank = pool[i].Rank,
                Position = (new Point(j % cols, j / cols) * Card.Size).ToVector2(),
            };
        }

        Random.Shared.Shuffle(cards);

        for (int i = 0, j = count; i < count; ++i, ++j)
        {
            cards[i].Position = (new Point(i % cols, i / cols) * Card.Size).ToVector2();
            cards[j].Position = (new Point(j % cols, j / cols) * Card.Size).ToVector2();
        }

        return cards;
    }
}
