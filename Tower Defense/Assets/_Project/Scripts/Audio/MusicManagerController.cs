using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class MusicManagerController : MonoBehaviour
{
    private static MusicManagerController _instance;
    public static MusicManagerController Instance => _instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }

    [field: SerializeField]
    public AudioMixer GlobalMixer { get; private set; }
    public VolumeControl MusicVolume { get; private set; }
    public VolumeControl SoundVolume { get; private set; }

    [SerializeField]
    private MusicTrackController _currentTrack;
    public MusicTrackController CurrentTrack
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
            _currentTrack.transform.parent = transform;
        }
    }

    void Awake()
    {
        // Application.persistentDataPath works in WebGL builds along with System.IO.File.WriteAllText
        // string path = System.IO.Path.Combine(Application.persistentDataPath, "settings.cfg");
        // System.IO.File.WriteAllText(path, "Test");
        if (_instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        _instance = this;
        // MusicVolume = new VolumeControl("MusicVolume", GlobalMixer);
        // SoundVolume = new VolumeControl("SoundVolume", GlobalMixer);
        DontDestroyOnLoad(gameObject);
    }
}