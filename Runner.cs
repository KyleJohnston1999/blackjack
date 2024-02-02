namespace blackjack;

public class Runner : IRunner
{
    List<Card> CardDeck = new List<Card>();
    GameState GameState = null;
    public GameState Hit()
    {
        Card card = CardDeck.Last();
        CardDeck.Remove(card);
        GameState.PlayerCards.Add(card);
        return GameState;
    }

    private void HitDealer() {
        Card card = CardDeck.Last();
        CardDeck.Remove(card);
        GameState.DealerCards.Add(card);
    }

    public GameState StartGame(int bet)
    {
        GameState = new GameState(bet);
        GenerateCards();
        Hit();
        Hit();
        HitDealer();
        return GameState;
    }

    public GameState Stay()
    {
        // calculate the dealer hand
        throw new NotImplementedException();
    }

    public GameState Surrender()
    {
        GameState.CurrentBid += GameState.InitialBid;
        GameState.Done = true;
        return GameState;
    }

    private void GenerateCards() 
    {
        CardSuit[] suits =  { CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Clubs, CardSuit.Spades };
        CardDeck.Clear();

        foreach (var suit in suits)
        {
            CardDeck.Add(new Card(1, CardType.Ace, suit));
            for (int Value = 2; Value < 11; Value++)
            {
                CardDeck.Add(new Card(Value, CardType.Numeric, suit));
            }
            CardDeck.Add(new Card(10, CardType.Ace, suit));
            CardDeck.Add(new Card(10, CardType.Ace, suit));
            CardDeck.Add(new Card(10, CardType.Ace, suit));
        }

        CardDeck.Shuffle();
    }
}
