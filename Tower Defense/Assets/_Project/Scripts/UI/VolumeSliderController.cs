using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public sealed class VolumeSliderController : MonoBehaviour
{

    [SerializeField]
    private VolumeControl _volumeControl;
    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Start()
    {
        _volumeControl.LoadPlayerPrefs();
        _slider.onValueChanged.AddListener(value => _volumeControl.Volume = value);
    }

    public void SetValue(float value) => _slider.value = value;

    void OnEnable()
    {
        _volumeControl.OnChanged += SetValue;
    }

    void OnDisable()
    {
        _volumeControl.OnChanged -= SetValue;
    }
}
