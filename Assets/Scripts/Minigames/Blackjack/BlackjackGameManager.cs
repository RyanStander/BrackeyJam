using System;
using System.Collections;
using System.Collections.Generic;
using Minigames.Blackjack.Visuals;
using Spine.Unity;
using UnityEngine;

namespace Minigames.Blackjack
{
    /// <summary>
    /// The Blackjack manager handles the actions the player can input as well as running through the game logic
    /// </summary>
    public class BlackjackGameManager : MonoBehaviour
    {
        [SerializeField] private DealerAnimationHandler _dealerAnimationHandler;
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
        [SerializeField] private HeartDisplay _playerHealthDisplay;
        [SerializeField] private HeartDisplay _dealerHealthDisplay;
        [SerializeField] private GameObject _hitStayUI;
        [SerializeField] private Animator _hitStayUIAnimator;

        [SerializeField] private OptionalBetDisplay _bettingUI;
        private void OnValidate()
        {
            if (_hitStayUIAnimator == null && _hitStayUI != null)
                _hitStayUIAnimator = _hitStayUI.GetComponent<Animator>();
            
            if(_bettingUI == null)
                _bettingUI = FindObjectOfType<OptionalBetDisplay>();
        }

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

        //TODO: when the game starts, dealer will explain the basics of blackjack

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

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            //TODO: Move health to the Centre of the board
            _playerBetThisRound = 1; //forced bet
            _playerHealthDisplay.MoveHeartsToBetPositions(_playerBetThisRound);
            _dealerHealthDisplay.MoveHeartsToBetPositions(_playerBetThisRound);

            yield return DealPlayerCardWithAnimation();

            yield return new WaitForSeconds(1f);

            yield return DealDealerCardWithAnimation(false);

            FinalBetting();
        }

        private IEnumerator DealPlayerCardWithAnimation()
        {
            _dealerAnimationHandler.PlayDeal();
            yield return new WaitUntil(() => _dealerAnimationHandler.AddCardToTable());
            _dealerAnimationHandler.ResetAddCardToTable();
            DealPlayerCard(_deck[_cardIndex++]);
        }

        private IEnumerator DealDealerCardWithAnimation(bool faceUp)
        {
            _dealerAnimationHandler.PlayDeal();
            yield return new WaitUntil(() => _dealerAnimationHandler.AddCardToTable());
            _dealerAnimationHandler.ResetAddCardToTable();
            DealDealerCard(_deck[_cardIndex++], faceUp);
        }

        private void FinalBetting()
        {
            if (_playerCurrentHealth <= 1 || _dealerCurrentHealth <= 1)
                StartCoroutine(SecondDeal());
            else
            {
                _bettingUI.SetActive(true);
                _bettingUI.PlayOptionalBetShowAnimation(_playerCurrentHealth,_dealerCurrentHealth);
            }
            
        }

        public void IncreaseBet(int amount)
        {
            _playerBetThisRound += amount;
            _playerHealthDisplay.MoveHeartsToBetPositions(amount);
            _dealerHealthDisplay.MoveHeartsToBetPositions(amount);
            StartCoroutine(SecondDeal());
        }

        private IEnumerator SecondDeal()
        {
            if (_playerCurrentHealth > 1 && _dealerCurrentHealth > 1)
                _bettingUI.PlayOptionalBetHideAnimation();
            yield return DealPlayerCardWithAnimation();
            _bettingUI.SetActive(false);
            yield return new WaitForSeconds(1f);
            yield return DealDealerCardWithAnimation(true);

            PromptHitStay();
        }

        private void PromptHitStay()
        {
            _hitStayUI.SetActive(true);
            _hitStayUIAnimator.Play("HitStayShow");
        }

        public void Hit()
        {
            StartCoroutine(HitCoroutine());
        }

        private IEnumerator HitCoroutine()
        {
            _hitStayUIAnimator.Play("HitStayHide");


            yield return DealPlayerCardWithAnimation();
            //we dont wait to wait if this finishes after the animation.
            yield return new WaitUntil(() => _hitStayUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("HitStayHide") &&
                                             _hitStayUIAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            _hitStayUI.SetActive(false);
            if (_playerHand.GetHandValue() > 21)
            {
                DealerWinsRound();
            }
            else if (_playerHand.CardCount == 5)
            {
                PlayerWinsRound();
            }
            else
                PromptHitStay();
        }

        public void Stay()
        {
            _hitStayUI.SetActive(false);
            StartCoroutine(StartDealerTurn());
        }

        private IEnumerator StartDealerTurn()
        {
            //TODO: Make a nice reveal anim
            _dealerHand.RevealHand();
            while (true)
            {
                if (_dealerHand.GetHandValue() < 17)
                {
                    yield return DealDealerCardWithAnimation(true);
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
            StartCoroutine(PlayerWinsRoundWithAnimation());

            Debug.Log($"Player Health: {_playerCurrentHealth}, Dealer Health: {_dealerCurrentHealth}");
        }

        private IEnumerator PlayerWinsRoundWithAnimation()
        {
            yield return new WaitForSeconds(2);
            _dealerAnimationHandler.PlaySad();
            yield return new WaitUntil(() => _dealerAnimationHandler.SlappedTable());
            _dealerAnimationHandler.ResetSlappedTable();
            _dealerHealthDisplay.QuickRemoveHeart();
            _playerHealthDisplay.QuickReturnHearts();
            _playerHand.ClearHand();
            _dealerHand.ClearHand();
            _dealerCurrentHealth -= _playerBetThisRound;

            yield return new WaitUntil(() => _dealerAnimationHandler.ChangeFace());
            _dealerAnimationHandler.ResetChangeFace();

            yield return new WaitForSeconds(2f);

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
            StartCoroutine(DealerWinsRoundWithAnimation());

            Debug.Log($"Player Health: {_playerCurrentHealth}, Dealer Health: {_dealerCurrentHealth}");
        }

        private IEnumerator DealerWinsRoundWithAnimation()
        {
            yield return new WaitForSeconds(2);
            _dealerAnimationHandler.PlayHappy();
            yield return new WaitUntil(() => _dealerAnimationHandler.HappyFace());
            _dealerAnimationHandler.ResetHappyFace();
            _dealerHealthDisplay.SlowReturnHearts();
            _playerHealthDisplay.SlowShrinkHeart();
            _playerCurrentHealth -= _playerBetThisRound;

            yield return new WaitForSeconds(2f);

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
