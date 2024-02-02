namespace blackjack;

public class GameState
{
    public GameState(int bet) {
        InitialBid = bet;
        CurrentBid = bet;
    }
    public List<Card> PlayerCards { get; set; } = List<>();
    public List<Card> DealerCards { get; set; } = List<>();
    public int InitialBid { get; set; };
    public int CurrentBid { get; set; };
    public bool Done { get; set; } = false;
}
