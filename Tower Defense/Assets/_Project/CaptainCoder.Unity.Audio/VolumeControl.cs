using UnityEngine;
using UnityEngine.Audio;

namespace CaptainCoder.Unity.Audio
{
    [CreateAssetMenu(fileName = "VolumeControl", menuName = "TD/Audio/VolumeControl")]
    public class VolumeControl : ScriptableObject
    {
        [field: SerializeField]
        public AudioMixer Mixer { get; private set; }
        [field: SerializeField]
        public string ExposedParameter { get; private set; } = "Unnamed";
        [field: SerializeField]
        public float DefaultVolume { get; private set; } = 0.7f;
        private event System.Action<float> _onChanged;
        public event System.Action<float> OnChanged
        {
            add
            {
                _onChanged += value;
                value.Invoke(_volume);
            }
            remove => _onChanged -= value;
        }
        [SerializeField]
        private float _volume;
        public float Volume
        {
            get => _volume;
            set
            {
                if (_volume == value) { return; }
                _volume = Mathf.Clamp01(value);
                Mixer.SetFloat(ExposedParameter, PercentToDb(_volume));
                PlayerPrefs.SetFloat(ExposedParameter, _volume);
                _onChanged?.Invoke(_volume);
            }
        }

        public void LoadPlayerPrefs()
        {
            Volume = PlayerPrefs.GetFloat(ExposedParameter, DefaultVolume);
            Mixer.SetFloat(ExposedParameter, PercentToDb(Volume));
        }

        public static float DbToPercent(float db)
        {
            if (db < -20) { return 0; }
            return Mathf.Clamp01(1 - (db / -40));
        }

        public static float PercentToDb(float percent)
        {
            if (percent <= 0) { return -80; }
            return Mathf.Lerp(-40, 0, percent);
        }
    }
}