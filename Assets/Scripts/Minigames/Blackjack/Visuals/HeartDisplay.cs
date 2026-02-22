using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Blackjack.Visuals
{
    public class HeartDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hearts;
        private Vector3[] _heartOriginalPositions;
        [SerializeField] private Transform[] _heartBetTransforms;
        private int _currentBetHearts;
        private int _heartsLeft;

        private void Awake()
        {
            _heartsLeft = _hearts.Length;
            
            _heartOriginalPositions = new Vector3[_hearts.Length];
            for (int i = 0; i < _hearts.Length; i++)
            {
                _heartOriginalPositions[i] = _hearts[i].transform.localPosition;
            }
        }

        public void MoveHeartsToBetPositions(int betHearts)
        {
            for (int i = 0; i < betHearts; i++)
            {
                int heartIndex = _heartsLeft - 1 - _currentBetHearts - i;
                int betIndex = _currentBetHearts + i;

                StartCoroutine(MoveHeart(
                    _hearts[heartIndex],
                    _heartBetTransforms[betIndex].position,
                    0.5f));
            }

            _currentBetHearts += betHearts;
        }

        private IEnumerator MoveHeart(GameObject heart, Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = heart.transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                heart.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            heart.transform.position = targetPosition;
        }

        #region Player Win Actions

        public void QuickRemoveHeart()
        {
            for (int i = 0; i < _currentBetHearts; i++)
            {
                int heartIndex = _heartsLeft - 1 - i;
                _hearts[heartIndex].SetActive(false);
            }

            _heartsLeft -= _currentBetHearts;
            _currentBetHearts = 0;
        }
        
        public void QuickReturnHearts()
        {
            for (int i = 0; i < _currentBetHearts; i++)
            {
                int heartIndex = _heartsLeft - 1 - i;

                _hearts[heartIndex].SetActive(true);
                _hearts[heartIndex].transform.localPosition =
                    _heartOriginalPositions[heartIndex];
            }

            _currentBetHearts = 0;
        }

        #endregion

        #region Player Lose Actions

        public void SlowShrinkHeart()
        {
            StartCoroutine(SlowShrinkRoutine());
        }

        private IEnumerator SlowShrinkRoutine()
        {
            int betCount = _currentBetHearts;
            
            for (int i = 0; i < betCount; i++)
            {
                int heartIndex = _heartsLeft - 1 - i;
                StartCoroutine(ShrinkHeart(_hearts[heartIndex], 0.5f));
            }

            // Wait for duration (since all shrink coroutines run same time)
            yield return new WaitForSeconds(0.5f);

            _heartsLeft -= betCount;
            _currentBetHearts = 0;
        }

        private IEnumerator ShrinkHeart(GameObject heart, float duration)
        {
            Vector3 originalScale = heart.transform.localScale;
            Vector3 targetScale = Vector3.zero;
            float elapsed = 0f;
            
            heart.GetComponent<Animator>().enabled = false;

            while (elapsed < duration)
            {
                heart.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            heart.transform.localScale = targetScale;
            heart.SetActive(false);
        }
        
        public void SlowReturnHearts()
        {
            StartCoroutine(SlowReturnRoutine());
        }

        private IEnumerator SlowReturnRoutine()
        {
            int betCount = _currentBetHearts;
            float duration = 0.5f;

            for (int i = 0; i < betCount; i++)
            {
                int heartIndex = _heartsLeft - 1 - i;
                int betIndex = betCount - 1 - i;

                StartCoroutine(SlowSlideHearts(
                    _hearts[heartIndex],
                    _heartOriginalPositions[heartIndex],
                    duration));
            }

            yield return new WaitForSeconds(duration);

            _currentBetHearts = 0;
        }
        
        private IEnumerator SlowSlideHearts(GameObject heart, Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = heart.transform.localPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                heart.transform.localPosition =
                    Vector3.Lerp(startPosition, targetPosition, elapsed / duration);

                elapsed += Time.deltaTime;
                yield return null;
            }

            heart.transform.localPosition = targetPosition;
        }

        #endregion

        
    }
}
