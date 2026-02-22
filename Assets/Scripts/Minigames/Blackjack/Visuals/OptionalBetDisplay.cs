using UnityEngine;

namespace Minigames.Blackjack.Visuals
{
    public class OptionalBetDisplay : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _allInButton;

        public void PlayOptionalBetShowAnimation(int playerRemainingHearts, int dealerRemainingHearts)
        {
            if(playerRemainingHearts==2 || dealerRemainingHearts==2)
                _allInButton.SetActive(false);
            
            _animator.Play("OptionalBetShow");
        }
        
        public void PlayOptionalBetHideAnimation()
        {
            _animator.Play("OptionalBetHide");
        }
        
        public void SetActive(bool active)
        {
            _animator.gameObject.SetActive(active);
        }
    }
}
