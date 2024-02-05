namespace blackjack;

public class GameState
{
    public GameState(int bet) {
        InitialBid = bet;
        CurrentBid = bet;
    }
    public List<Card> PlayerCards { get; set; } = new List<Card>();
    public List<Card> DealerCards { get; set; } = new List<Card>();
    public int InitialBid { get; set; }
    public int CurrentBid { get; set; }
    public bool Done { get => IsStay || IsBust || IsSurrender; }
    public bool IsStay { get; set; } = false;
    public bool IsBust { get; set; } = false;
    public bool IsSurrender { get; set; } = false;
    public bool IsDoubleDownAllowed { get; set; } = false;
    public int PlayerTotal { get => PlayerCards.Aggregate(0, (sum, next) => sum + next.Value); }
    public int DealerTotal { get => DealerCards.Aggregate(0, (sum, next) => sum + next.Value); }
}
