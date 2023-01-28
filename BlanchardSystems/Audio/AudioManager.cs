using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BlanchardSystems.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Tooltip("Get volume from PlayerPrefs to setup mixers, toggles and sliders")]
        [SerializeField] private bool _initializeOnStart;

        [Space]
        [SerializeField] private AudioMixer _mixerFX;
        [SerializeField] private AudioMixer _mixerBGM;

        [SerializeField] private Slider _sliderVolumeFX;
        [SerializeField] private Slider _sliderVolumeBGM;

        [SerializeField] private ToggleButton[] _toggleFX;
        [SerializeField] private ToggleButton[] _toggleBGM;

        [Space]
        [Tooltip("Audio Sources to Pause and Resume")]
        [SerializeField] private List<AudioSource> _audioSources;

        private float _defaultVolume = 0.45f;

        private bool _enabledFX;
        private bool _enabledBGM;

        private void Start()
        {
            if (_initializeOnStart)
                Initialize();
        }

        public void SetVolumeFXMixer(float value)
        {
            var volume = ConvertValue(value);
            _mixerFX.SetFloat("Volume", volume);
            PlayerPrefs.SetFloat("VolumeFX", value);
            _enabledFX = value > 0f;
            InitializeTogglesVolumes();
        }

        public void SetVolumeBGMMixer(float value)
        {
            var volume = ConvertValue(value);
            _mixerBGM.SetFloat("Volume", volume);
            PlayerPrefs.SetFloat("VolumeBGM", value);
            _enabledBGM = value > 0;
            InitializeTogglesVolumes();
        }

        private float ConvertValue(float value)
        {
            var volume = 0f;
            if (value < 0.5f)
            {
                volume = value * 160 - 80;
            }

            if (value >= 0.5f)
            {
                volume = value * 40 - 20;
            }

            return volume;
        }

        public void Initialize()
        {
            var valueFX = PlayerPrefs.GetFloat("VolumeFX", _defaultVolume);
            SetVolumeFXMixer(valueFX);

            var valueBGM = PlayerPrefs.GetFloat("VolumeBGM", _defaultVolume);
            SetVolumeBGMMixer(valueBGM);

            InitializeSliderVolumeBGM(valueBGM);
            InitializeSliderVolumeFX(valueFX);

        }

        private void InitializeSliderVolumeFX(float valueFX)
        {
            if (_sliderVolumeFX != null)
            {
                _sliderVolumeFX.value = valueFX;
                _sliderVolumeFX.onValueChanged.AddListener(SetVolumeFXMixer);
            }
        }

        private void InitializeSliderVolumeBGM(float valueBGM)
        {
            if (_sliderVolumeBGM != null)
            {
                _sliderVolumeBGM.value = valueBGM;
                _sliderVolumeBGM.onValueChanged.AddListener(SetVolumeBGMMixer);
            }
        }

        private void InitializeTogglesVolumes()
        {
            if (_toggleFX.Length > 0)
            {
                foreach (var toggle in _toggleFX)
                {
                    toggle.off.SetActive(!_enabledFX);
                }
            }

            if (_toggleBGM.Length > 0)
            {
                foreach (var toggle in _toggleBGM)
                {
                    toggle.off.SetActive(!_enabledBGM);
                }
            }
        }

        public void ToggleAudioMixerFX()
        {
            var value = _enabledFX ? 0f : _defaultVolume;
            _enabledFX = !_enabledFX;
            if (_sliderVolumeFX != null)
            {
                _sliderVolumeFX.value = value;
            }
            else
            {
                InitializeTogglesVolumes();
                SetVolumeFXMixer(value);
            }
        }

        public void ToggleAudioMixerBGM()
        {
            var value = _enabledBGM ? 0f : _defaultVolume;
            _enabledBGM = !_enabledBGM;
            InitializeTogglesVolumes();
            if (_sliderVolumeBGM != null)
            {
                _sliderVolumeBGM.value = value;
            }
            else
            {
                SetVolumeBGMMixer(value);
            }
        }


        public void PauseAllAudios()
        {
            if (_audioSources != null)
            {
                if (_audioSources.Count > 0)
                {
                    foreach (var audioSource in _audioSources)
                    {
                        audioSource.Pause();
                    }
                }
            }
        }

        public void UnPauseAllAudios()
        {
            if (_audioSources != null)
            {
                if (_audioSources.Count > 0)
                {
                    foreach (var audioSource in _audioSources)
                    {
                        audioSource.UnPause();
                    }
                }
            }
        }


    }

    [Serializable]
    public class ToggleButton
    {
        public GameObject on;
        public GameObject off;
    }
}