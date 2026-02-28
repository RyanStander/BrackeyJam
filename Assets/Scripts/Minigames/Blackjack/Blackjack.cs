using System;
using System.Collections.Generic;
using System.Linq;

namespace Minigames.Blackjack
{
    public class Blackjack
    {
        private int _playerMaxHealth = 3;
        private int _playerCurrentHealth;
        private int _dealerMaxHealth = 3;
        private int _dealerCurrentHealth;
        private int _playerBetThisRound;

        private int _deckCount = 1;
        private List<Card> _deck;
        private int _cardIndex = 0;

        private List<Card> _playerHand = new();
        private List<(Card card, bool faceUp)> _dealerHand = new();
        private bool _dealerCardFaceUp;

        public Blackjack(List<Card> deck)
        {
            _deck = deck;
            _playerCurrentHealth = _playerMaxHealth;
            _dealerCurrentHealth = _dealerMaxHealth;
        }

        public void ResetAndShuffleDeck()
        {
            _deck.Clear();
            _playerHand.Clear();
            _dealerHand.Clear();
            _playerCurrentHealth = _playerMaxHealth;
            _dealerCurrentHealth = _dealerMaxHealth;
            _cardIndex = 0;

            for (int deckCountNum = 0; deckCountNum < _deckCount; deckCountNum++)
            {
                _deck.AddRange(CreateDeck());
            }

            ShuffleDeck();
        }

        private IEnumerable<Card> CreateDeck()
        {
            return (from CardSuit suit in Enum.GetValues(typeof(CardSuit))
                from CardRank rank in Enum.GetValues(typeof(CardRank))
                select new Card { Suit = suit, Rank = rank }).ToList();
        }

        private void ShuffleDeck()
        {
            for (int i = 0; i < _deck.Count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, _deck.Count);
                (_deck[i], _deck[randomIndex]) = (_deck[randomIndex], _deck[i]);
            }
        }

        public Card DealCardToPlayer()
        {
            Card cardDraw = _deck[_cardIndex++];

            _playerHand.Add(cardDraw);
            return cardDraw;
        }

        public Card DealCardToDealer()
        {
            Card cardDraw = _deck[_cardIndex++];

            _dealerHand.Add((cardDraw, _dealerHand.Count != 0));
            return cardDraw;
        }

        public void IncreaseBet(int amount)
        {
            if(amount<1)
                return;
            
            _playerBetThisRound = Math.Min(_playerBetThisRound + amount, _playerCurrentHealth);
        }

        public int GetCurrentBet() => _playerBetThisRound;

        public void ApplyDamage(bool toPlayer)
        {
            if (toPlayer)
                _playerCurrentHealth = Math.Max(0, _playerCurrentHealth - _playerBetThisRound);
            else
                _dealerCurrentHealth = Math.Max(0, _dealerCurrentHealth - _playerBetThisRound);

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
            if (GetPlayerHandValue() > 21)
                return RoundResult.DealerWin;
            
            if (GetDealerHandValue() > 21)
                return RoundResult.PlayerWin;

            if (_playerHand.Count == 5)
                return RoundResult.PlayerWin;
            
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

        private int GetHandValue(List<Card> hand)
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

        private int GetHandValue(List<(Card card, bool faceUp)> handWithHiddenCards, bool countHiddenCard = true)
        {
            List<Card> pureCards = handWithHiddenCards.Where(x => countHiddenCard || x.faceUp).Select(x => x.card)
                .ToList();

            return GetHandValue(pureCards);
        }

        public bool CanPlayerHit() => _playerHand.Count < 5 && GetPlayerHandValue() < 21;

        public bool ShouldDealerHit() =>
            (GetDealerHandValue() < 17 || DoesDealerHaveSoft17()) && _dealerHand.Count < 5;

        private bool DoesDealerHaveSoft17() => GetDealerHandValue() == 17 &&
                                               _dealerHand.Any(dealerCard => dealerCard.card.Rank == CardRank.Ace);

        public bool IsGameOver() => _playerCurrentHealth <= 0 || _dealerCurrentHealth <= 0;
    }
}
