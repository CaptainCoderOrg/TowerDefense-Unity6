#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CaptainCoder.Unity.Audio
{
    [CreateAssetMenu(fileName = "MusicTrackManager", menuName = "TD/Music Track Manager")]
    public class MusicTrackManager : ScriptableObject
    {
        [SerializeField]
        private MusicTrackController _currentTrack;
        public MusicTrackController Track
        {
            get => _currentTrack;
            set
            {
                Debug.Log($"Setting Track: {_currentTrack} => {value}");
                if (_currentTrack != null)
                {
                    if (_currentTrack.Clip == value.Clip)
                    {
                        GameObject.Destroy(value.gameObject);
                        return;
                    }
                    GameObject fadingOut = _currentTrack.gameObject;
                    _currentTrack.FadeOut();
                    _currentTrack.OnFadeOutFinished.AddListener(() => GameObject.Destroy(fadingOut));
                }
                _currentTrack = value;
                _currentTrack.FadeIn();
                DontDestroyOnLoad(_currentTrack); 
            }
        }


#if UNITY_EDITOR
        private void OnEnable()
        {
            // Subscribe for the "event"
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        }

        private void OnDisable()
        {
            // Don't forget to unsubscribe
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            Debug.Log($"PlayModeChanged: {state}");
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                _currentTrack = null;
            }
        }
#endif
    }
}