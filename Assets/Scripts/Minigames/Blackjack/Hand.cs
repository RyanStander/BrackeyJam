using System;
using TMPro;
using UnityEngine;

namespace Minigames.Blackjack
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private HandCard[] _handCards;
        [SerializeField] private float _cardFadeInDuration = 0.5f;
        [SerializeField] private TextMeshProUGUI _handValueText; 
        private int _cardCount;
        private CardData _cardData;
        
        private void Awake()
        {
            foreach (HandCard handCard in _handCards)
            {
                handCard.CardDisplay.gameObject.SetActive(false);
            }
            
            _handValueText.text = "0";
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
            StartCoroutine(handCard.CardDisplay.FadeInCard(_cardFadeInDuration));
            handCard.Revealed = reveal;
            _cardCount++;
            
            UpdateHandValueText();
        }

        public void ClearHand()
        {
            foreach (HandCard handCard in _handCards)
            {
                handCard.CardDisplay.gameObject.SetActive(false);
                handCard.Reset();
            }

            _cardCount = 0;
            _handValueText.text = "0";
        }

        public int GetHandValue(bool countHiddenCard = true)
        {
            int value = 0;
            int aceCount = 0;
            for (int index = 0; index < _cardCount; index++)
            {
                HandCard card = _handCards[index];
                if (!countHiddenCard && !card.Revealed)
                    continue;
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

                handCard.CardDisplay.RevealCard();
                handCard.Revealed = true;
            }
        }
        
        private void UpdateHandValueText()
        {
            int handValue = GetHandValue(false);
            _handValueText.text = handValue.ToString();
        }
        
        public int CardCount => _cardCount;
        
        public void SetCardData(CardData cardData) => _cardData = cardData;
    }
}
