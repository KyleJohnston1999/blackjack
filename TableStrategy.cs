using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack
{
    public class TableStrategy : IStrategy
    {
        private bool isFirstRound = true;
        public int GetInitialBet()
        {
            return 10;
        }

        public Move GetMove(GameState gameState)
        {
            if (!gameState.IsDoubleDownAllowed) isFirstRound = false;
            return GetMoveBasedOnShownCards(gameState.PlayerTotal, gameState.PlayerCards, gameState.DealerCards.First().Value);
        }

        private Move GetMoveBasedOnShownCards(int playerTotal, List<Card> playerCards, int dealerCard)
        {
            if (isFirstRound)
            {
                if (playerCards.Where(x => x.Type == CardType.Ace).Count() > 0)
                    return GetMoveBasedOnAce(playerCards, dealerCard);
            }

            return GetMoveBasedOnTotal(playerTotal, dealerCard);

        }

        private Move GetMoveBasedOnAce(List<Card> playerCards, int dealerCard)
        {   
            int otherPlayerCard = playerCards.ElementAt(0).Value == 1 ?  playerCards.ElementAt(0).Value : playerCards.ElementAt(1).Value;

            switch(otherPlayerCard)
            {
                case < 4:if (dealerCard > 4 && dealerCard < 7) return Move.DoubleDown;
                    break;
                case < 6: if (dealerCard > 3 && dealerCard < 7) return Move.DoubleDown;
                    break;
                case <8: if (dealerCard > 1 && dealerCard < 7)return Move.DoubleDown; 
                    break;
            }
            switch (otherPlayerCard)
            {
                case < 7: return Move.Hit;
                case 7: if (dealerCard > 8) return Move.Hit;
                    break;
            }
            return Move.Stay;
        }

        private Move GetMoveBasedOnTotal(int playerTotal, int dealerCard)
        {
            if (isFirstRound)
            {
                switch (playerTotal)
                {
                    case 9:
                        if (dealerCard >= 3 && dealerCard <= 6) return Move.DoubleDown;
                        break;
                    case 10:
                        if (dealerCard <= 9) return Move.DoubleDown;
                        break;
                    case 11: return Move.DoubleDown;
                    case 15:
                        if (dealerCard == 10) return Move.Surrender;
                        break;
                    case 16:
                        if (dealerCard >= 9) return Move.Surrender;
                        break;
                }
            }

            switch (playerTotal)
            {
                case < 12: return Move.Hit;
                case 12: if (dealerCard >= 4 && dealerCard <= 6) return Move.Stay; else return Move.Hit;
                case < 17: if (dealerCard < 7) return Move.Stay; else return Move.Hit;
            }
            return Move.Stay;

        }
    }
}
