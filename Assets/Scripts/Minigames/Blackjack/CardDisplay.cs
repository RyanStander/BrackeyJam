using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Blackjack
{
    public class CardDisplay : MonoBehaviour
    {
        private Card _card;
        private CardData _cardData;
        [SerializeField] private Image _cardFrontImage;
        [SerializeField] private Image _cardRankImage;
        [SerializeField] private Image _cardSuitImage;

        public void SetCard(CardData cardData, CardRank rank, CardSuit suit, bool showCard = true)
        {
            _card.Rank = rank;
            _card.Suit = suit;
            _cardData = cardData;

            if (showCard)
                ShowCard();
            else
            {
                _cardFrontImage.sprite = _cardData.CardBackSprite;
                _cardFrontImage.color = Color.white;
                _cardRankImage.sprite = _cardData.CardBackSprite;
                _cardRankImage.color = Color.white;
                _cardSuitImage.sprite = _cardData.CardBackSprite;
                _cardSuitImage.color = Color.white;
            }
        }

        public void ShowCard()
        {
            if (_cardData == null)
            {
                Debug.LogError("CardData is not set for this CardDisplay.");
                return;
            }

            _cardFrontImage.sprite = _cardData.CardFrontSprite;
            _cardFrontImage.color = _cardData.CardColor;
            Debug.Log(_card.Rank + " as " + (int)_card.Rank);
            _cardRankImage.sprite = _cardData.RankSprites[_card.Rank];
            _cardRankImage.color = _cardData.RankColor;
            _cardSuitImage.sprite = _cardData.SuitSprites[_card.Suit];
            _cardSuitImage.color = _cardData.SuitColor;
        }

        public void Reset()
        {
            _card = default;
            _cardData = null;
            _cardFrontImage.sprite = null;
            _cardRankImage.sprite = null;
            _cardSuitImage.sprite = null;
        }

        public Card Card => _card;
    }
}
