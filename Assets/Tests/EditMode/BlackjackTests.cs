using System.Collections.Generic;
using Minigames.Blackjack;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class BlackjackTests
    {
        private Blackjack _blackjack;

        [SetUp]
        public void Setup()
        {
            Card tenOfClubs = new(CardRank.Ten, CardSuit.Clubs);
            Card threeOfHearts = new(CardRank.Three, CardSuit.Hearts);
            List<Card> tempDeck = new()
            {
                tenOfClubs,
                tenOfClubs,
                threeOfHearts,
                tenOfClubs,
                tenOfClubs
            };
            _blackjack = new Blackjack(tempDeck);
        }

        #region Hand Value Tests

        [Test]
        public void PlayerCardDealtMatches()
        {
            Card givenCard = _blackjack.DealCardToPlayer();
            Card tenOfClubs = new(CardRank.Ten, CardSuit.Clubs);

            Assert.AreEqual(tenOfClubs, givenCard);
        }
        
        [Test]
        public void DealerCardDealtMatches()
        {
            Card givenCard = _blackjack.DealCardToDealer();
            Card tenOfClubs = new(CardRank.Ten, CardSuit.Clubs);

            Assert.AreEqual(tenOfClubs, givenCard);
        }

        [Test]
        public void GetPlayerHandValue_Returns20()
        {
            _blackjack.DealCardToPlayer();
            _blackjack.DealCardToPlayer();

            Assert.AreEqual(20, _blackjack.GetPlayerHandValue());
        }

        [Test]
        public void GetVisibleDealerHandValue_WithHiddenCard_ReturnsOnlyFaceUpValue()
        {
            _blackjack.DealCardToDealer();
            _blackjack.DealCardToDealer();

            Assert.AreEqual(10, _blackjack.GetDealerHandValue(false));
        }

        [Test]
        public void TwoAces_Returns12()
        {
            Card aceOfSpades = new(CardRank.Ace, CardSuit.Spades);
            List<Card> tempDeck = new()
            {
                aceOfSpades,
                aceOfSpades
            };
            Blackjack tempBlackjack = new(tempDeck);

            tempBlackjack.DealCardToPlayer();
            tempBlackjack.DealCardToPlayer();

            Assert.AreEqual(12, tempBlackjack.GetPlayerHandValue());
        }
        
        [Test]
        public void DealerHiddenCard_Visibility_Works()
        {
            Card aceOfSpades = new(CardRank.Ace, CardSuit.Spades);
            Card sixOfClubs = new(CardRank.Six, CardSuit.Clubs);
            List<Card> tempDeck = new() { aceOfSpades, sixOfClubs };
            Blackjack tempBlackjack = new(tempDeck);

            tempBlackjack.DealCardToDealer(); // hidden Ace
            tempBlackjack.DealCardToDealer(); // visible Six

            Assert.AreEqual(6, tempBlackjack.GetDealerHandValue(false)); // only visible
            Assert.AreEqual(17, tempBlackjack.GetDealerHandValue(true)); // includes hidden
        }

        [Test]
        public void Player_A_A_9_Equals21()
        {
            Card ace = new(CardRank.Ace, CardSuit.Spades);
            Card nine = new(CardRank.Nine, CardSuit.Hearts);
            List<Card> tempDeck = new() { ace, ace, nine };
            Blackjack bj = new(tempDeck);

            bj.DealCardToPlayer();
            bj.DealCardToPlayer();
            bj.DealCardToPlayer();

            Assert.AreEqual(21, bj.GetPlayerHandValue());
        }

        [Test]
        public void Player_A_9_9_Equals19()
        {
            Card ace = new(CardRank.Ace, CardSuit.Spades);
            Card nineH = new(CardRank.Nine, CardSuit.Hearts);
            Card nineC = new(CardRank.Nine, CardSuit.Clubs);
            List<Card> tempDeck = new() { ace, nineH, nineC };
            Blackjack bj = new(tempDeck);

            bj.DealCardToPlayer();
            bj.DealCardToPlayer();
            bj.DealCardToPlayer();

            Assert.AreEqual(19, bj.GetPlayerHandValue());
        }

        #endregion

        #region Bust Detection Tests

        [Test]
        public void PlayerFiveCardsButBust()
        {
            for (int i = 0; i < 5; i++)
            {
                _blackjack.DealCardToPlayer();
            }

            Assert.AreEqual(false, _blackjack.DoesPlayerHaveFiveCards());
        }
        
        [Test]
        public void PlayerBust()
        {
            for (int i = 0; i < 5; i++)
            {
                _blackjack.DealCardToPlayer();
            }

            Assert.AreEqual(true, _blackjack.IsPlayerBust());
        }

        [Test]
        public void DealerBust()
        {
            for (int i = 0; i < 5; i++)
            {
                _blackjack.DealCardToDealer();
            }

            Assert.AreEqual(true, _blackjack.IsDealerBust());
        }

        #endregion

        #region Blackjack Detection Tests
        
        [Test]
        public void DoesPlayerHaveBlackjack()
        {
            Card aceOfSpades = new(CardRank.Ace, CardSuit.Spades);
            Card tenOfClubs = new(CardRank.Ten, CardSuit.Clubs);
            List<Card> tempDeck = new()
            {
                aceOfSpades,
                tenOfClubs
            };
            Blackjack tempBlackjack = new(tempDeck);

            tempBlackjack.DealCardToPlayer();
            tempBlackjack.DealCardToPlayer();

            Assert.AreEqual(true, tempBlackjack.DoesPlayerHaveBlackjack());
        }
        
        [Test]
        public void DoesDealerHaveBlackjack()
        {
            Card aceOfSpades = new(CardRank.Ace, CardSuit.Spades);
            Card tenOfClubs = new(CardRank.Ten, CardSuit.Clubs);
            List<Card> tempDeck = new()
            {
                aceOfSpades,
                tenOfClubs
            };
            Blackjack tempBlackjack = new(tempDeck);

            tempBlackjack.DealCardToDealer();
            tempBlackjack.DealCardToDealer();

            Assert.AreEqual(true, tempBlackjack.DoesDealerHaveBlackjack());
        }
        
        #endregion

        #region Round Result Tests

        [Test]
        public void PlayerFiveCardsWin()
        {
            Card aceOfSpades = new(CardRank.Ace, CardSuit.Spades);
            List<Card> tempDeck = new()
            {
                aceOfSpades,
                aceOfSpades,
                aceOfSpades,
                aceOfSpades,
                aceOfSpades
            };
            Blackjack tempBlackjack = new(tempDeck);

            for (int i = 0; i < 5; i++)
            {
                tempBlackjack.DealCardToPlayer();
            }

            Assert.AreEqual(true, tempBlackjack.DoesPlayerHaveFiveCards());
            Assert.AreEqual(RoundResult.PlayerWin,tempBlackjack.DetermineRoundResult());
        }
        
        [Test]
        public void PlayerWins()
        {
            _blackjack.DealCardToPlayer();
            _blackjack.DealCardToPlayer();
            _blackjack.DealCardToDealer();
            _blackjack.DealCardToDealer();

            Assert.AreEqual(RoundResult.PlayerWin, _blackjack.DetermineRoundResult());
        }

        [Test]
        public void DealerWins()
        {
            _blackjack.DealCardToDealer();
            _blackjack.DealCardToDealer();
            _blackjack.DealCardToPlayer();
            _blackjack.DealCardToPlayer();

            Assert.AreEqual(RoundResult.DealerWin, _blackjack.DetermineRoundResult());
        }

        [Test]
        public void PushRound()
        {
            _blackjack.DealCardToPlayer();
            _blackjack.DealCardToDealer();

            Assert.AreEqual(RoundResult.Push, _blackjack.DetermineRoundResult());
        }
        
        [Test]
        public void DetermineRoundResult_BlackjackPush()
        {
            Card ace = new(CardRank.Ace, CardSuit.Spades);
            Card ten = new(CardRank.Ten, CardSuit.Clubs);

            // Player blackjack
            Blackjack bj = new(new List<Card> { ace, ten, ace, ten });
            bj.DealCardToPlayer();
            bj.DealCardToPlayer();

            // Dealer blackjack
            bj.DealCardToDealer();
            bj.DealCardToDealer();

            Assert.AreEqual(RoundResult.Push, bj.DetermineRoundResult());
        }

        [Test]
        public void DetermineRoundResult_PlayerBJ_BeatsDealer20()
        {
            Card ace = new(CardRank.Ace, CardSuit.Spades);
            Card ten = new(CardRank.Ten, CardSuit.Clubs);
            Card nine = new(CardRank.Nine, CardSuit.Hearts);
            Card jack = new(CardRank.Jack, CardSuit.Diamonds);

            Blackjack bj = new(new List<Card> { ace, ten, nine, jack });
            bj.DealCardToPlayer();
            bj.DealCardToPlayer(); // Player 21 blackjack
            bj.DealCardToDealer();
            bj.DealCardToDealer(); // Dealer 19

            Assert.AreEqual(RoundResult.PlayerWin, bj.DetermineRoundResult());
        }

        [Test]
        public void DetermineRoundResult_DealerBJ_BeatsPlayer20()
        {
            Card ace = new(CardRank.Ace, CardSuit.Spades);
            Card ten = new(CardRank.Ten, CardSuit.Clubs);
            Card ten2 = new(CardRank.Ten, CardSuit.Diamonds);
            Blackjack bj = new(new List<Card> { ace, ten, ten2, ten });

            // Dealer blackjack
            bj.DealCardToDealer();
            bj.DealCardToDealer();

            // Player 20
            bj.DealCardToPlayer();
            bj.DealCardToPlayer();

            Assert.AreEqual(RoundResult.DealerWin, bj.DetermineRoundResult());
        }

        [Test]
        public void DetermineRoundResult_PlayerBust_Loses()
        {
            Card ten = new(CardRank.Ten, CardSuit.Clubs);
            Card three = new (CardRank.Three, CardSuit.Clubs);
            Blackjack bj = new(new List<Card> { ten, ten, ten, ten, ten,three,three });

            bj.DealCardToPlayer();
            bj.DealCardToPlayer();
            bj.DealCardToPlayer();
            bj.DealCardToPlayer();
            bj.DealCardToPlayer();
            
            bj.DealCardToDealer();
            bj.DealCardToDealer();

            Assert.AreEqual(true, bj.IsPlayerBust());
            Assert.AreEqual(RoundResult.DealerWin, bj.DetermineRoundResult());
        }

        #endregion

        #region Bet Tests

        [Test]
        public void BetMaxAmount()
        {
            _blackjack.IncreaseBet(10);

            Assert.AreEqual(3,_blackjack.GetCurrentBet());
        }

        [Test]
        public void DealDamageToPlayer()
        {
            _blackjack.IncreaseBet(3);
            _blackjack.ApplyDamage(true);
            
            Assert.AreEqual(true,_blackjack.IsGameOver());
        }
        
        [Test]
        public void DealDamageToDealer()
        {
            _blackjack.IncreaseBet(3);
            _blackjack.ApplyDamage(false);
            
            Assert.AreEqual(true,_blackjack.IsGameOver());
        }

        [Test]
        public void BetNegative()
        {
            _blackjack.IncreaseBet(-10);
            
            Assert.AreEqual(0,_blackjack.GetCurrentBet());
        }

        [Test]
        public void Bet0()
        {
            _blackjack.IncreaseBet(1);
            _blackjack.IncreaseBet(0);
            
            Assert.AreEqual(1,_blackjack.GetCurrentBet());
        }

        #endregion

        #region Hit Tests

        [Test]
        public void CanPlayerHit_5Cards()
        {
            for (int i = 0; i < 5; i++)
            {
                _blackjack.DealCardToPlayer();
            }

            Assert.AreEqual(false, _blackjack.CanPlayerHit());
        }
        
        [Test]
        public void CanPlayerHit_Bust()
        {
            for (int i = 0; i < 3; i++)
            {
                _blackjack.DealCardToPlayer();
            }

            Assert.AreEqual(false, _blackjack.CanPlayerHit());
        }

        [Test]
        public void ShouldDealerHit_5Cards()
        {
            for (int i = 0; i < 5; i++)
            {
                _blackjack.DealCardToDealer();
            }

            Assert.AreEqual(false, _blackjack.ShouldDealerHit());
        }
        
        [Test]
        public void Dealer_ShouldHit_OnSoft17_ButNotOnHard17()
        {
            // Soft 17: A + 6 => should hit
            {
                Card ace = new(CardRank.Ace, CardSuit.Spades);
                Card six = new(CardRank.Six, CardSuit.Clubs);
                Blackjack bj = new(new List<Card> { ace, six });
                bj.DealCardToDealer();
                bj.DealCardToDealer();
                Assert.IsTrue(bj.ShouldDealerHit());
            }

            // Hard 17: 10 + 7 => should stand
            {
                Card ten = new(CardRank.Ten, CardSuit.Clubs);
                Card seven = new(CardRank.Seven, CardSuit.Hearts);
                Blackjack bj = new(new List<Card> { ten, seven });
                bj.DealCardToDealer();
                bj.DealCardToDealer();
                Assert.IsFalse(bj.ShouldDealerHit());
            }
        }
        
        [Test]
        public void CanPlayerHit_Boundaries()
        {
            // Exactly 21 with two cards => cannot hit
            {
                Card ace = new(CardRank.Ace, CardSuit.Spades);
                Card ten = new(CardRank.Ten, CardSuit.Clubs);
                Blackjack bj = new(new List<Card> { ace, ten });
                bj.DealCardToPlayer();
                bj.DealCardToPlayer();
                Assert.IsFalse(bj.CanPlayerHit());
            }

            // Four cards totaling 20 => can hit
            {
                Card five = new(CardRank.Five, CardSuit.Clubs);
                Blackjack bj = new(new List<Card> { five, five, five, five });
                bj.DealCardToPlayer();
                bj.DealCardToPlayer();
                bj.DealCardToPlayer();
                bj.DealCardToPlayer();
                Assert.AreEqual(20, bj.GetPlayerHandValue());
                Assert.IsTrue(bj.CanPlayerHit());
            }

            // Four cards totaling 21 => cannot hit
            {
                Card five = new(CardRank.Five, CardSuit.Clubs);
                Card six = new(CardRank.Six, CardSuit.Hearts);
                Blackjack bj = new(new List<Card> { five, five, five, six });
                bj.DealCardToPlayer();
                bj.DealCardToPlayer();
                bj.DealCardToPlayer();
                bj.DealCardToPlayer();
                Assert.AreEqual(21, bj.GetPlayerHandValue());
                Assert.IsFalse(bj.CanPlayerHit());
            }
        }

        #endregion
        
    }
}
