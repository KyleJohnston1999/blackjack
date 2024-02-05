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
        if (GameState.PlayerTotal > 21) {
            GameState.IsBust = true;
        }
        GameState.IsDoubleDownAllowed = false;
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
        GameState.IsDoubleDownAllowed = true;
        return GameState;
    }

   public GameState Stay()
    {
        HitDealer();
        // check if over or below 16 and hit again
        while (GameState.DealerTotal <= 16) {
            HitDealer();
        }
        GameState.IsStay = true;
        return GameState;
    }

    public GameState Surrender()
    {
        GameState.IsSurrender = true;
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
            CardDeck.Add(new Card(10, CardType.King, suit));
            CardDeck.Add(new Card(10, CardType.Queen, suit));
            CardDeck.Add(new Card(10, CardType.Jack, suit));
        }

        CardDeck.Shuffle();
    }

    public GameState DoubleDown()
    {
        if (GameState.IsDoubleDownAllowed) {
            GameState.CurrentBid *= 2;
        }
        Hit();
        return GameState;
    }
}
