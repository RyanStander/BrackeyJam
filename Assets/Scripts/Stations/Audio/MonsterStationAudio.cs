using System;
using System.Collections;
using AudioManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Stations.Audio
{
    public class MonsterStationAudio : MonoBehaviour
    {
        [SerializeField] private float _monsterSoundInterval = 10f;

        private void Start()
        {
            StartCoroutine(PlayMonsterSounds());
        }

        private IEnumerator PlayMonsterSounds()
        {
            yield return new WaitForSeconds(3f);
            while (true)
            {
                AudioManager.PlayOneShot(PersistentManager.AudioDataHandler.StationMonster.Idle());

                yield return new WaitForSeconds(_monsterSoundInterval+Random.Range(-_monsterSoundInterval*0.5f, _monsterSoundInterval*0.5f));
            }
        }
        
        public void SteppedOn()
        {
            AudioManager.PlayOneShot(PersistentManager.AudioDataHandler.StationMonster.Squish());
        }
    }
}
