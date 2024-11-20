using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class AbstractVolumeSliderController : MonoBehaviour
{
    private Slider _slider;

    protected abstract VolumeControl VolumeControl { get; }
    
    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Start()
    {
        _slider.onValueChanged.AddListener(value => VolumeControl.Volume = value);
    }

    public void SetValue(float value) => _slider.value = value;

    void OnEnable()
    {
        StartCoroutine(RegisterOnMusicManager());
    }

    private IEnumerator RegisterOnMusicManager()
    {
        var condition = new WaitUntil(() => MusicManagerController.Instance != null);
        yield return condition;
        SetValue(VolumeControl.Volume);
        VolumeControl.OnChanged += SetValue;
    }

    void OnDisable()
    {
        VolumeControl.OnChanged -= SetValue;
    }
}
