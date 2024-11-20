using UnityEngine;

namespace CaptainCoder.Unity.Audio
{
    [CreateAssetMenu(fileName = "MusicTrackManager", menuName = "TD/Music Track Manager")]
    public class MusicTrackManager : ScriptableObject
    {
        private MusicTrackController _currentTrack;
        public MusicTrackController Track
        {
            get => _currentTrack;
            set
            {
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
    }
}