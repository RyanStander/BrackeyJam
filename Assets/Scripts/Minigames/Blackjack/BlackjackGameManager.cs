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
        [SerializeField] private Hand _playerHand;
        [SerializeField] private Hand _dealerHand;
        [SerializeField] private CardData _playerCardData;
        [SerializeField] private CardData _dealerCardData;
        [SerializeField] private GameObject _bettingUI;
        [SerializeField] private GameObject _hitStayUI;

        private void Start()
        {
            Initialise();
            StartNewRound();
        }

        private void Initialise()
        {
            _playerCurrentHealth = _playerMaxHealth;
            _dealerCurrentHealth = _dealerMaxHealth;
            _playerHand.SetCardData(_playerCardData);
            _dealerHand.SetCardData(_dealerCardData);
        }

        private void StartNewRound()
        {
            _playerHand.ClearHand();
            _dealerHand.ClearHand();

            int index = 0;
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    _deck[index] = new Card { Suit = suit, Rank = rank };
                    index++;
                }
            }

            //shuffle
            for (int i = 0; i < _deck.Length; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, _deck.Length);
                (_deck[i], _deck[randomIndex]) = (_deck[randomIndex], _deck[i]);
            }

            StartGame();
        }

        private void StartGame()
        {
            _playerBetThisRound = 1; //forced bet

            //deal cards
            DealPlayerCard(_deck[_cardIndex++]);
            DealDealerCard(_deck[_cardIndex++], false);

            FinalBetting();
        }

        private void FinalBetting()
        {
            _bettingUI.SetActive(true);
        }

        public void IncreaseBet(int amount)
        {
            _playerBetThisRound += amount;
            SecondDeal();
            _bettingUI.SetActive(false);
        }

        private void SecondDeal()
        {
            DealPlayerCard(_deck[_cardIndex++]);
            DealDealerCard(_deck[_cardIndex++], true);

            PromptHitStay();
        }

        private void PromptHitStay()
        {
            _hitStayUI.SetActive(true);
        }

        public void Hit()
        {
            DealPlayerCard(_deck[_cardIndex++]);
            _hitStayUI.SetActive(false);

            if (_playerHand.GetHandValue() > 21)
            {
                DealerWinsRound();
            }
            else
                PromptHitStay();
        }

        public void Stay()
        {
            _hitStayUI.SetActive(false);
            StartDealerTurn();
        }

        private void StartDealerTurn()
        {
            _dealerHand.RevealHand();
            while (true)
            {
                if (_dealerHand.GetHandValue() < 17)
                {
                    DealDealerCard(_deck[_cardIndex++], true);
                    continue;
                }

                EvaluateWinner();

                break;
            }
        }

        private void EvaluateWinner()
        {
            if (_dealerHand.GetHandValue() > 21)
            {
                PlayerWinsRound();
                return;
            }
            
            bool playerHasBlackjack = _playerHand.HasBlackjack();
            bool dealerHasBlackjack = _dealerHand.HasBlackjack();

            if (playerHasBlackjack && dealerHasBlackjack)
                PushRound();
            else if (playerHasBlackjack)
                PlayerWinsRound();
            else if (dealerHasBlackjack)
                DealerWinsRound();
            else if (_playerHand.GetHandValue() > _dealerHand.GetHandValue())
                PlayerWinsRound();
            else if (_playerHand.GetHandValue() < _dealerHand.GetHandValue())
                DealerWinsRound();
            else
                PushRound();
        }

        private void DealPlayerCard(Card card)
        {
            _playerHand.ReceiveCard(card);
        }

        private void DealDealerCard(Card card, bool faceUp)
        {
            _dealerHand.ReceiveCard(card, faceUp);
        }

        private void PlayerWinsRound()
        {
            _dealerCurrentHealth -= _playerBetThisRound;
            if (_dealerCurrentHealth <= 0)
            {
                //player wins the game
            }
            else
                StartNewRound();

            Debug.Log($"Player Health: {_playerCurrentHealth}, Dealer Health: {_dealerCurrentHealth}");
        }

        private void DealerWinsRound()
        {
            _playerCurrentHealth -= _playerBetThisRound;
            if (_playerCurrentHealth <= 0)
            {
                //dealer wins the game
            }
            else
                StartNewRound();

            Debug.Log($"Player Health: {_playerCurrentHealth}, Dealer Health: {_dealerCurrentHealth}");
        }

        private void PushRound()
        {
            //show push results, then start new round
            StartNewRound();
        }
    }
}
