using System.Collections;
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
        _slider.onValueChanged.AddListener(value => _musicManager.MusicVolume.Volume = value);
    }

    public void SetValue(float value) => _slider.value = value;

    void OnEnable()
    {
        StartCoroutine(RegisterOnMusicManager());
    }

    private IEnumerator RegisterOnMusicManager()
    {
        var condition = new WaitUntil(() => _musicManager != null);
        yield return condition;
        SetValue(_musicManager.MusicVolume.Volume);
        _musicManager.MusicVolume.OnChanged += SetValue;
    }

    void OnDisable()
    {
        _musicManager.MusicVolume.OnChanged -= SetValue;
    }
}
