using System;
using System.Collections;
using AudioManagement;
using PersistentManager;
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
            AudioManager.PlayMusic(AudioDataHandler.StationMonster.MonsterMusic());
            AudioManager.PlayAmbience(AudioDataHandler.StationMonster.MonsterAmbience());
        }

        private IEnumerator PlayMonsterSounds()
        {
            yield return new WaitForSeconds(3f);
            while (true)
            {
                AudioManager.PlayOneShot(AudioDataHandler.StationMonster.Idle());

                yield return new WaitForSeconds(_monsterSoundInterval+Random.Range(-_monsterSoundInterval*0.5f, _monsterSoundInterval*0.5f));
            }
        }
        
        public void SteppedOn()
        {
            AudioManager.PlayOneShot(AudioDataHandler.StationMonster.SquishStep());
        }
    }
}
