namespace Minigames.Blackjack
{
    [System.Serializable]
    public class HandCard
    {
        public CardDisplay CardDisplay;
        public bool Revealed;
        
        public void Reset()
        {
            CardDisplay.gameObject.SetActive(false);
            Revealed = false;
            CardDisplay.Reset();
        }
    }
}
