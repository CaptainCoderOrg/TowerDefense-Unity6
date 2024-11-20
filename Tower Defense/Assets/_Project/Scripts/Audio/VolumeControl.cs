using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl
{
    public string Name { get; private set;}
    public float DefaultVolume { get; private set; }
    public AudioMixer Mixer { get; private set; }
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
    private float _volume;
    public float Volume
    {
        get => _volume;
        set
        {
            if (_volume == value) { return; }
            _volume = Mathf.Clamp01(value);
            Mixer.SetFloat(Name, PercentToDb(_volume));
            PlayerPrefs.SetFloat(Name, _volume);
            _onChanged?.Invoke(_volume);
        }
    }

    public VolumeControl(string name, AudioMixer mixer, float defaultVolume = 0.7f)
    {
        Mixer = mixer;
        Name = name;
        Volume = PlayerPrefs.GetFloat(Name, defaultVolume);
        DefaultVolume = defaultVolume;
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