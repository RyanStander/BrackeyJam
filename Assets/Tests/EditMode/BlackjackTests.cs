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
            Card tenOfClubs = new (CardRank.Ten, CardSuit.Clubs);
            List<Card> tempDeck = new()
            {
                tenOfClubs,
                tenOfClubs
            };
            _blackjack = new Blackjack(tempDeck);
        }
        
        
        [Test]
        public void GetPlayerHandValue_Returns20()
        {  
            _blackjack.DealCardToPlayer();
            _blackjack.DealCardToPlayer();
            
            Assert.AreEqual(20,_blackjack.GetPlayerHandValue());
        }

        [Test]
        public void GetVisibleDealerHandValue_WithHiddenCard_ReturnsOnlyFaceUpValue()
        {
            _blackjack.DealCardToDealer();
            _blackjack.DealCardToDealer();
            
            Assert.AreEqual(10,_blackjack.GetDealerHandValue(false));
        }
    }
}
