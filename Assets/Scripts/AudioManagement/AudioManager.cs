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
        
        #region Static Wrappers

        /// <summary>
        /// Plays a one-shot sound effect. Use this for sounds that don't need to be stopped or have parameters changed after being played.
        /// </summary>
        /// <param name="sound">Use AudioDataHandler.[LIBRARY].soundName</param>
        public static void PlayOneShot(EventReference sound)
        {
            RuntimeManager.PlayOneShot(sound);
        }

        /// <summary>
        ///  Plays music. If the same music is already playing, it won't restart. If different music is playing, it will stop the current music and start the new one.
        /// </summary>
        /// <param name="music">Use AudioDataHandler.[LIBRARY].musicName</param>
        public static void PlayMusic(EventReference music)
        {
            if (Instance == null) return;
            Instance.PlayMusicInstance(music);
        }

        /// <summary>
        /// Stops the currently playing music. If fadeOut is true, the music will fade out instead of stopping immediately.
        /// </summary>
        /// <param name="fadeOut">Whether to allow the music to fade out or stop it immediately.</param>
        public static void StopMusic(bool fadeOut = true)
        {
            if (Instance == null) return;
            Instance.StopMusicInstance(fadeOut);
        }
        
        /// <summary>
        /// Plays a sound effect with optional parameters. Use this for sounds that need to have parameters set or changed after being played. Parameters should be passed as tuples of (parameterName, parameterValue).
        /// </summary>
        /// <param name="sound">Use AudioDataHandler.[LIBRARY].soundName</param>
        /// <param name="parameters">Tuples of (parameterName, parameterValue) to set on the sound instance. For example: ("Intensity", 0.5f), ("IsAlert", 1f)</param>
        public static void Play(EventReference sound, params (string name, float value)[] parameters)
        {
            if (Instance == null) return;
         
            Instance.PlayInstance(sound, parameters);
        }

        #endregion

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

        private void PlayInstance(EventReference sound, params (string name, float value)[] parameters)
        {
            EventInstance instance = RuntimeManager.CreateInstance(sound);

            foreach ((string name, float value) parameter in parameters)
            {
                instance.setParameterByName(parameter.name, parameter.value);
            }

            instance.start();
            instance.release();
        }

        #endregion

        
    }
}
