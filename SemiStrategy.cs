using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack
{
    public class SemiStrategy : IStrategy
    {
        public Move GetMove(GameState gameState)
        {
            int playerScore = gameState.PlayerCards.Aggregate(0, (sum, next) => sum + next.Value);
            int dealerScore = gameState.DealerCards.Aggregate(0, (sum, next) => sum + next.Value);

            if (playerScore>= 5 && playerScore <= 8)
            {
                return Move.Hit;
            }

            if (playerScore == 11 || (playerScore == 10 && dealerScore < 10))
            {
                return Move.DoubleDown;
            }

            if ((playerScore == 13 && dealerScore < 7) || (playerScore == 14 && dealerScore < 7) || (playerScore == 15 && dealerScore < 7) || (playerScore == 9 && dealerScore < 7))
            {
                return Move.Stay;
            }

            if (playerScore > 17)
            {
                return Move.Stay;
            }
            
            return Move.Hit;
        }

        public int GetInitialBet()
        {
           return 10;
        }

    }
}
