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
        private EventInstance _currentAmbience;
        private EventReference _currentMusicReference;
        private EventReference _currentAmbienceReference;

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
        ///  Plays ambience. If the same ambience is already playing, it won't restart. If different ambience is playing, it will stop the current ambience and start the new one.
        /// </summary>
        /// <param name="ambience">Use AudioDataHandler.[LIBRARY].ambienceName</param>
        public static void PlayAmbience(EventReference ambience)
        {
            if (Instance == null) return;
            Instance.PlayAmbienceInstance(ambience);
        }
        
        /// <summary>
        ///  Stops the currently playing ambience. If fadeOut is true, the ambience will fade out instead of stopping immediately.
        /// </summary>
        /// <param name="fadeOut"> Whether to allow the ambience to fade out or stop it immediately.</param>
        public static void StopAmbience(bool fadeOut = true)
        {
            if (Instance == null) return;
            Instance.StopAmbienceInstance(fadeOut);
        }
        
        /// <summary>
        /// Plays a sound effect with optional parameters. Use this for sounds that need to have parameters set or changed after being played. Parameters should be passed as tuples of (parameterName, parameterValue).
        /// You can view parameters by opening FMOD top left where the File button is in unity editor->event browser->events and then you can navigate through there, if you click on sounds, some may have params at the bottom, you can change it to preview the effect.
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
        
        private void PlayAmbienceInstance(EventReference ambience)
        {
            if (_currentAmbienceReference.Equals(ambience))
                return;

            StopAmbienceInstance();

            _currentAmbience = RuntimeManager.CreateInstance(ambience);
            _currentAmbience.start();

            _currentAmbienceReference = ambience;
        }

        private void StopAmbienceInstance(bool fadeOut = true)
        {
            if (!_currentAmbience.isValid())
                return;

            _currentAmbience.stop(
                fadeOut
                    ? STOP_MODE.ALLOWFADEOUT
                    : STOP_MODE.IMMEDIATE
            );

            _currentAmbience.release();
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
