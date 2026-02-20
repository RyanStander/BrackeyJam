using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FadeToBlack : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeSpeed = 0.5f;
        private Coroutine _fadeInCoroutine;
        private Coroutine _fadeOutCoroutine;

        public void StartFadeToBlack()
        {
            if (_fadeInCoroutine != null || _fadeOutCoroutine != null)
            {
                StopCoroutine(_fadeInCoroutine);
                StopCoroutine(_fadeOutCoroutine);
            }

            _fadeInCoroutine = StartCoroutine(FadeIn());
        }

        public bool IsFading() => _fadeInCoroutine != null;

        private IEnumerator FadeIn()
        {
            Color color = _image.color;
            float alpha = 0;

            while (alpha < 1)
            {
                alpha += _fadeSpeed * Time.deltaTime;
                color.a = alpha;
                _image.color = color;
                yield return null;
            }
            _fadeOutCoroutine = StartCoroutine(FadeOut());
            _fadeInCoroutine = null;
        }

        private IEnumerator FadeOut()
        {
            Color color = _image.color;
            float alpha = 1;

            while (alpha > 0)
            {
                alpha -= _fadeSpeed * Time.deltaTime;
                color.a = alpha;
                _image.color = color;
                yield return null;
            }

            _fadeOutCoroutine = null;
        }
    }
}
