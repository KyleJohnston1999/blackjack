using System.Reflection.Metadata.Ecma335;

namespace blackjack;

public class BasicStrategy : IStrategy
{
    public Move GetMove(GameState gameState)
    {
        int playerScore = gameState.PlayerCards.Aggregate(0, (sum, next) => sum + next.Value);
        return playerScore > 16 ? Move.Stay : Move.Hit;
    }

    public int GetInitialBet() {
        return 10;
    }
}
