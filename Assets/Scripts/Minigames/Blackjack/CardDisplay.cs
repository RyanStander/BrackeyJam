using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Blackjack
{
    public class CardDisplay : MonoBehaviour
    {
        private Card _card;
        [SerializeField] private Image _cardFrontImage;
        [SerializeField] private Image _cardRankImage;
        [SerializeField] private Image _cardSuitImage;

        //TODO: Remove
        #region Temporary DELETE

        [SerializeField] private CardData _cardData;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            CardRank rank = (CardRank)UnityEngine.Random.Range(0, Enum.GetValues(typeof(CardRank)).Length);
            CardSuit suit = (CardSuit)UnityEngine.Random.Range(0, Enum.GetValues(typeof(CardSuit)).Length);
            SetCard(_cardData, rank, suit);
        }

        #endregion
        
        public void SetCard(CardData cardData, CardRank rank, CardSuit suit)
        {
            _cardFrontImage.sprite = cardData.CardFrontSprite;
            _cardFrontImage.color = cardData.CardColor;
            _cardRankImage.sprite = cardData.RankSprites[rank];
            _cardRankImage.color = cardData.RankColor;
            _cardSuitImage.sprite = cardData.SuitSprites[suit];
            _cardSuitImage.color = cardData.SuitColor;
            
            _card.Rank = rank;
            _card.Suit = suit;
        }
    }
}
