using System.Collections;
using UI;
using UnityEngine;

namespace TrainNavigation
{
    public class TravelUIHandler : MonoBehaviour
    {
        [SerializeField] private float _warmupTime = 3f;
        [SerializeField] private Animator _trainAnimator;
        [SerializeField] private FadeToBlack _fadeToBlack;
        private Coroutine _travelCoroutine;

        public void StartTravelAnimation()
        {
            if (_travelCoroutine != null)
            {
                StopCoroutine(_travelCoroutine);
            }

            _travelCoroutine = StartCoroutine(TravelAnimationCoroutine());
        }

        private IEnumerator TravelAnimationCoroutine()
        {
            yield return new WaitForSeconds(_warmupTime);
            
            _trainAnimator.Play("TrainJump");
            
            yield return new WaitUntil(() => _trainAnimator.GetCurrentAnimatorStateInfo(0).IsName("NewState"));
            
            _fadeToBlack.StartFadeToBlack();

            yield return new WaitUntil(() => !_fadeToBlack.IsFading());

            _travelCoroutine = null;
        }

        public bool ReadyToWarp()
        {
            return _travelCoroutine==null;
        }
    }
}
