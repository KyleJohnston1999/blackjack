namespace blackjack;

public class Result
{
    public int InitialBet { get; set; }
    public int Earnings { get; set; }
    public bool IsVictory { get; set; }
    public override string ToString() {
        return "Initial Bet: " + InitialBet + "; Earnings: " + Earnings + "; Did I win?: " + (IsVictory ? "YES! You are an absolute trooper" : "NO! You are lame and suck");
    }
}
