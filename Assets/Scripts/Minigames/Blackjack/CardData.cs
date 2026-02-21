using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Minigames.Blackjack
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Minigames/Blackjack/CardData", order = 0)]
    public class CardData : ScriptableObject
    {
        public GameObject CardPrefab;
        [Header("Ranks")]
        public SerializedDictionary<CardRank,Sprite> RankSprites;
        [Header("Suits")]
        public SerializedDictionary<CardSuit, Sprite> SuitSprites;
        [Header("Other")]
        public Sprite CardBackSprite;
        public Sprite CardFrontSprite;
        public Sprite JokerSprite;
        [Header("Colors")]
        public Color CardColor;
        public Color RankColor;
        public Color SuitColor;
    }
}
