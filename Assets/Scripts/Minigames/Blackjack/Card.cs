namespace Minigames.Blackjack
{
    public struct Card
    {
        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public CardRank Rank;
        public CardSuit Suit;
    }
}
