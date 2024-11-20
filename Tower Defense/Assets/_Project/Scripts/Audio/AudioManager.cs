using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioManager", menuName = "TD/Audio Manager")]
public class AudioManager : ScriptableObject
{
    [field: SerializeField]
    public AudioMixer Mixer { get; private set; }
    [SerializeField]
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