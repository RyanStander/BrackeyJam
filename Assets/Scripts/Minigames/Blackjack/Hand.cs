using System;
using UnityEngine;

namespace Minigames.Blackjack
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private HandCard[] _handCards;
        private int _cardCount;
        private CardData _cardData;
        
        private void Awake()
        {
            foreach (HandCard handCard in _handCards)
            {
                handCard.CardDisplay.gameObject.SetActive(false);
            }
        }
        

        public void ReceiveCard(Card card, bool reveal = true)
        {
            if (_cardCount >= _handCards.Length)
            {
                Debug.LogError("Hand is full, cannot receive more cards.");
                return;
            }

            HandCard handCard = _handCards[_cardCount];
            handCard.CardDisplay.SetCard(_cardData, card.Rank, card.Suit, reveal);
            handCard.CardDisplay.gameObject.SetActive(true);
            handCard.Revealed = reveal;
            _cardCount++;
        }

        public void ClearHand()
        {
            foreach (HandCard handCard in _handCards)
            {
                handCard.CardDisplay.gameObject.SetActive(false);
                handCard.Reset();
            }

            _cardCount = 0;
        }

        public int GetHandValue()
        {
            int value = 0;
            int aceCount = 0;
            for (int index = 0; index < _cardCount; index++)
            {
                HandCard card = _handCards[index];
                if (card.CardDisplay.Card.Rank == CardRank.Ace)
                    aceCount++;
                else
                    value += Math.Min((int)card.CardDisplay.Card.Rank, 10);
            }

            // Count all aces as 1 first
            value += aceCount;

            // Upgrade one ace to 11 if it doesn't bust
            if (aceCount > 0 && value + 10 <= 21)
                value += 10;

            return value;
        }
        
        public bool HasBlackjack()
        {
            if (_cardCount != 2)
                return false;

            Card card1 = _handCards[0].CardDisplay.Card;
            Card card2 = _handCards[1].CardDisplay.Card;

            bool firstIsAce = card1.Rank == CardRank.Ace;
            bool secondIsAce = card2.Rank == CardRank.Ace;

            bool firstIsTenValue = (int)card1.Rank >= 10;
            bool secondIsTenValue = (int)card2.Rank >= 10;

            return (firstIsAce && secondIsTenValue) ||
                   (secondIsAce && firstIsTenValue);
        }
        
        public void RevealHand()
        {
            for (int index = 0; index < _cardCount; index++)
            {
                HandCard handCard = _handCards[index];
                if (handCard.Revealed)
                    continue;

                handCard.CardDisplay.ShowCard();
                handCard.Revealed = true;
            }
        }
        
        public void SetCardData(CardData cardData) => _cardData = cardData;
    }
}
