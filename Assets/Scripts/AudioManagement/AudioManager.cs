using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace AudioManagement
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private EventInstance _currentMusic;
        private EventReference _currentMusicReference;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #region Instance Methods

        private void PlayMusicInstance(EventReference music)
        {
            if (_currentMusicReference.Equals(music))
                return;

            StopMusicInstance();

            _currentMusic = RuntimeManager.CreateInstance(music);
            _currentMusic.start();

            _currentMusicReference = music;
        }

        private void StopMusicInstance(bool fadeOut = true)
        {
            if (!_currentMusic.isValid())
                return;

            _currentMusic.stop(
                fadeOut
                    ? STOP_MODE.ALLOWFADEOUT
                    : STOP_MODE.IMMEDIATE
            );

            _currentMusic.release();
        }

        #endregion

        #region Static Wrappers

        /// <param name="sound">Use AudioDataHandler.[LIBRARY].soundName</param>
        public static void PlayOneShot(EventReference sound)
        {
            RuntimeManager.PlayOneShot(sound);
        }

        public static void PlayMusic(EventReference music)
        {
            if (Instance == null) return;
            Instance.PlayMusicInstance(music);
        }

        public static void StopMusic(bool fadeOut = true)
        {
            if (Instance == null) return;
            Instance.StopMusicInstance(fadeOut);
        }

        #endregion
    }
}
