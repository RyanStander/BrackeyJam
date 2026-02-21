using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Events;
using PersistentManager;
using EventType = Events.EventType;

namespace UI
{
    public class FuelGaugeDisplay : MonoBehaviour
    {
        [SerializeField] private Image[] _fuelCanIcons;
        [SerializeField] private float _deactivationDuration = 1f;
        [SerializeField] private float _blinkSpeed = 4f;

        private int _fuelLevel;
        private Coroutine _deactivateRoutine;

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.FuelChanged, OnFuelChanged);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.FuelChanged, OnFuelChanged);
        }

        private void Start()
        {
            _fuelLevel = Mathf.Clamp(TrainDataHandler.GetFuelLevel(), 0, _fuelCanIcons.Length);
            RefreshImmediate();
        }

        private void OnFuelChanged(EventData eventData)
        {
            if (!eventData.IsEventOfType(out FuelChanged _))
                return;

            UpdateFuelDisplay();
        }

        private void UpdateFuelDisplay()
        {
            int newLevel = Mathf.Clamp(TrainDataHandler.GetFuelLevel(), 0, _fuelCanIcons.Length);

            if (newLevel == _fuelLevel)
                return;

            if (_deactivateRoutine != null)
                StopCoroutine(_deactivateRoutine);

            if (newLevel > _fuelLevel)
            {
                for (int i = _fuelLevel; i < newLevel; i++)
                    _fuelCanIcons[i].gameObject.SetActive(true);
            }
            else
            {
                _deactivateRoutine = StartCoroutine(DeactivateRoutine(_fuelLevel, newLevel));
            }

            _fuelLevel = newLevel;
        }

        private IEnumerator DeactivateRoutine(int oldLevel, int newLevel)
        {
            float t = 0f;

            while (t < _deactivationDuration)
            {
                float alpha = Mathf.PingPong(t * _blinkSpeed, 1f);

                for (int i = newLevel; i < oldLevel; i++)
                {
                    Color c = _fuelCanIcons[i].color;
                    c.a = alpha;
                    _fuelCanIcons[i].color = c;
                }

                t += Time.deltaTime;
                yield return null;
            }

            for (int i = newLevel; i < oldLevel; i++)
                _fuelCanIcons[i].gameObject.SetActive(false);
        }

        private void RefreshImmediate()
        {
            for (int i = 0; i < _fuelCanIcons.Length; i++)
                _fuelCanIcons[i].gameObject.SetActive(i < _fuelLevel);
        }
    }
}
