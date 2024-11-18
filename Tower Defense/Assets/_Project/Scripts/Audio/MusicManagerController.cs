using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class MusicManagerController : MonoBehaviour
{

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