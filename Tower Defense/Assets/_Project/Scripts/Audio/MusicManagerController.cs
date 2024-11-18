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

    public UnityEvent<float> OnVolumeChanged;

    [field: SerializeField]
    public AudioMixer GlobalMixer { get; private set; }

    [SerializeField]
    private float _musicVolume;

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            GlobalMixer.SetFloat("MusicVolume", PercentToDb(_musicVolume));
            PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
            OnVolumeChanged.Invoke(_musicVolume);
        }
    }

    [SerializeField]
    private MusicTrackController _currentTrack;
    public MusicTrackController CurrentTrack
    {
        get => _currentTrack;
        set
        {
            if (_currentTrack != null)
            {
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
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    void Start()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }

    public static float DbToPercent(float db)
    {
        if (db < -20) { return 0; }
        return Mathf.Clamp01(1 - (db / -20));
    }

    public static float PercentToDb(float percent)
    {
        if (percent <= 0) { return -80; }
        return Mathf.Lerp(-20, 0, percent);
    }
}