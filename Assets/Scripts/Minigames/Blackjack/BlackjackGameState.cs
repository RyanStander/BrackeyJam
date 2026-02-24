using System;
using System.Collections.Generic;

namespace Minigames.Blackjack
{
    public class BlackjackGameState
    {
        private int _playerMaxHealth = 3;
        private int _playerCurrentHealth;
        private int _dealerMaxHealth = 3;
        private int _dealerCurrentHealth;
        private int _playerBetThisRound;

        private int _deckCount = 1;
        private List<Card> _deck = new();
        private int _cardIndex = 0;

        private List<Card> _playerHand = new();
        private List<Card> _dealerHand = new();
        private bool _dealerCardFaceUp;

        public void ResetAndShuffleDeck()
        {
            _deck.Clear();
            _playerHand.Clear();
            _dealerHand.Clear();
            _cardIndex = 0;

            for (int deckCountNum = 0; deckCountNum < _deckCount; deckCountNum++)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                    {
                        _deck.Add(new Card { Suit = suit, Rank = rank });
                    }
                }
            }

            //shuffle
            for (int i = 0; i < _deck.Count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, _deck.Count);
                (_deck[i], _deck[randomIndex]) = (_deck[randomIndex], _deck[i]);
            }
        }

        public Card DealCardToPlayer() => _deck[_cardIndex++];

        public Card DealCardToDealer(bool faceUp) => _deck[_cardIndex++];

        public void IncreaseBet(int amount) =>
            _playerBetThisRound = Math.Min(_playerBetThisRound + amount, _playerCurrentHealth);

        public void ApplyDamage(int damage, bool toPlayer)
        {
            if (toPlayer)
                _playerCurrentHealth = Math.Max(0, _playerCurrentHealth - damage);
            else
                _dealerCurrentHealth = Math.Max(0, _dealerCurrentHealth - damage);

            _playerBetThisRound = 0;
        }

        public int GetPlayerHandValue()
        {
            return GetHandValue(_playerHand);
        }

        public int GetDealerHandValue(bool countHiddenCard = true)
        {
            return GetHandValue(_dealerHand, countHiddenCard);
        }

        public bool IsPlayerBust() => GetPlayerHandValue() > 21;

        public bool IsDealerBust() => GetDealerHandValue() > 21;

        public bool DoesPlayerHaveBlackjack() => _playerHand.Count == 2 && GetPlayerHandValue() == 21;

        public bool DoesDealerHaveBlackjack() => _dealerHand.Count == 2 && GetDealerHandValue() == 21;

        public bool DoesPlayerHaveFiveCards() => _playerHand.Count == 5 && GetPlayerHandValue() <= 21;

        public RoundResult DetermineRoundResult()
        {
            if (GetDealerHandValue() > 21)
            {
                return RoundResult.PlayerWin;
            }

            bool playerHasBlackjack = DoesPlayerHaveBlackjack();
            bool dealerHasBlackjack = DoesDealerHaveBlackjack();

            if (playerHasBlackjack && dealerHasBlackjack)
                return RoundResult.Push;
            if (playerHasBlackjack)
                return RoundResult.PlayerWin;
            if (dealerHasBlackjack)
                return RoundResult.DealerWin;
            if (GetPlayerHandValue() > GetDealerHandValue())
                return RoundResult.PlayerWin;
            if (GetPlayerHandValue() < GetDealerHandValue())
                return RoundResult.DealerWin;
            return RoundResult.Push;
        }


        private int GetHandValue(List<Card> hand, bool countHiddenCard = true)
        {
            int value = 0;
            int aceCount = 0;
            foreach (Card card in hand)
            {
                if (card.Rank == CardRank.Ace)
                    aceCount++;
                else
                    value += Math.Min((int)card.Rank, 10);
            }

            // Count all aces as 1 first
            value += aceCount;

            // Upgrade one ace to 11 if it doesn't bust
            if (aceCount > 0 && value + 10 <= 21)
                value += 10;

            return value;
        }
        
        public bool CanPlayerHit() => _playerHand.Count < 5 && GetPlayerHandValue() < 21;
        
        public bool ShouldDealerHit() => GetDealerHandValue() < 17 || (GetDealerHandValue() == 17 && _dealerHand.Count < 5);
        
        bool IsGameOver() => _playerCurrentHealth <= 0 || _dealerCurrentHealth <= 0;
    }
}
