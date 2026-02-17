using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Blackjack
{
    /// <summary>
    /// The Blackjack manager handles the actions the player can input as well as running through the game logic
    /// </summary>
    public class BlackjackGameManager : MonoBehaviour
    {
        [SerializeField] private int _playerMaxHealth = 3;
        [SerializeField] private int _dealerMaxHealth = 3;
        private Card[] _deck = new Card[52];
        private int _cardIndex = 0;
        private int _playerBetThisRound;

        private int _playerCurrentHealth;
        private int _dealerCurrentHealth;
        private List<Card> _playerHand = new();
        private List<Card> _dealerHand = new();

        private void SetHealth()
        {
            _playerCurrentHealth = _playerMaxHealth;
            _dealerCurrentHealth = _dealerMaxHealth;
        }

        private void StartNewRound()
        {
            _playerHand.Clear();
            _dealerHand.Clear();

            int index = 0;
            for (int suit = 0; suit < Enum.GetValues(typeof(CardSuit)).Length; suit++)
            {
                for (int rank = 0; rank < Enum.GetValues(typeof(CardRank)).Length; rank++)
                {
                    _deck[index] = new Card { Suit = (CardSuit)suit, Rank = (CardRank)rank };
                    index++;
                }
            }

            //shuffle
            for (int i = 0; i < _deck.Length; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, _deck.Length);
                (_deck[i], _deck[randomIndex]) = (_deck[randomIndex], _deck[i]);
            }
        }

        private void StartGame()
        {
            _playerBetThisRound = 1; //forced bet

            //deal cards
            DealPlayerCard(_deck[_cardIndex++]);
            DealDealerCard(_deck[_cardIndex++], false);

            FinalBetting();
        }

        private void SecondDeal()
        {
            DealPlayerCard(_deck[_cardIndex++]);
            DealDealerCard(_deck[_cardIndex++], true);
        }

        private void FinalBetting()
        {
            //show button to bet or skip
        }

        private void PromptHitStays()
        {
            //show buttons for hit or stay, this continues until the player stays or busts
        }

        private void EvaluateWinner()
        {
            //compare player and dealer hand values, determine winner, update health, show results
            //dont forget pushing if the hand values are tied
        }

        private void DealPlayerCard(Card card)
        {
            _playerHand.Add(card);
        }

        private void DealDealerCard(Card card, bool faceUp)
        {
            _dealerHand.Add(card);
        }

        public void Hit()
        {
        }

        public void Stay()
        {
            StartDealerTurn();
        }

        private void StartDealerTurn()
        {
        }

        public void IncreaseBet(int amount)
        {
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
    }
}
