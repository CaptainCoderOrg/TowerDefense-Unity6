using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MusicVolumeSliderController : MonoBehaviour
{
    private MusicManagerController _musicManager => MusicManagerController.Instance;
    private Slider _slider;
    
    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Start()
    {
        _slider.onValueChanged.AddListener(value => _musicManager.MusicVolume = value);
    }

    public void SetValue(float value) => _slider.value = value;

    void OnEnable()
    {
        SetValue(_musicManager.MusicVolume);
        _musicManager.OnVolumeChanged.AddListener(SetValue);
    }

    void OnDisable()
    {
        _musicManager.OnVolumeChanged.RemoveListener(SetValue);
    }
}
