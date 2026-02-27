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

        [Test]
        public void PlayerCardDeltMatches()
        {
            Card givenCard = _blackjack.DealCardToPlayer();
            Card tenOfClubs = new(CardRank.Ten, CardSuit.Clubs);

            Assert.AreEqual(tenOfClubs, givenCard);
        }
        
        [Test]
        public void DealerCardDeltMatches()
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
        public void PlayerFiveCardsButBust()
        {
            for (int i = 0; i < 5; i++)
            {
                _blackjack.DealCardToPlayer();
            }

            Assert.AreEqual(false, _blackjack.DoesPlayerHaveFiveCards());
        }
        
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
        }

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
                _blackjack.DealCardToPlayer();
            }

            Assert.AreEqual(true, _blackjack.IsPlayerBust());
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
    }
}
