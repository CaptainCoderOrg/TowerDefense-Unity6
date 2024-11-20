using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CaptainCoder.Unity.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicTrackController : MonoBehaviour
    {
        [SerializeField]
        private MusicTrackManager _trackManager;
        public bool BecomeTrackOnStart = true;
        public UnityEvent OnFadeOutFinished;
        [field: SerializeField]
        public float FadeDuration { get; set; } = 5f;
        private AudioSource _audioSource;
        public AudioClip Clip => _audioSource?.clip;
        public bool IsPlaying => _audioSource == null ? false : _audioSource.isPlaying;
        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            if (BecomeTrackOnStart)
            {
                _trackManager.Track = this;
            }
        }

        private IEnumerator ChangeVolume(float startVolume, float endVolume, UnityEvent callback = null)
        {
            if (!_audioSource.isPlaying) { _audioSource.Play(); }
            float startTime = Time.time;
            float percent = 0;
            while (percent < 1)
            {
                percent = Mathf.Clamp01((Time.time - startTime) / FadeDuration);
                _audioSource.volume = Mathf.Lerp(startVolume, endVolume, percent);
                yield return null;
            }
            _audioSource.volume = endVolume;
            callback?.Invoke();
        }

        public void FadeIn()
        {
            StopAllCoroutines();
            StartCoroutine(ChangeVolume(0, 1));
        }

        public void FadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(ChangeVolume(_audioSource.volume, 0, OnFadeOutFinished));
        }
    }
}