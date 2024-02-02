namespace blackjack;

public struct Card
{
    public Card(int Value, CardType Type, CardSuit Suit)
    {
        this.Value = Value;
        this.Type = Type;
        this.Suit = Suit;
    }

    public int Value { get; set; }
    public CardType Type { get; set; }
    public CardSuit Suit { get; set; }
}
