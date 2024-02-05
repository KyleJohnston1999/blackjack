namespace blackjack;

public class Result
{
    public int InitialBet { get; set; }
    public int Earnings { get; set; }
    public Outcome Outcome { get; set; }
    public override string ToString() {
        return "Initial Bet: " + InitialBet + "; Earnings: " + Earnings + "; Did I win?: " + Outcome;
    }
}
