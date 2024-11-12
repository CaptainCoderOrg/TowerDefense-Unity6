using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AnimatedNumberText : MonoBehaviour
{
    public float Duration = 1;
    private TextMeshProUGUI _label;
    private float _startTime;
    [SerializeField]
    private int _displayedValue = 0;
    [SerializeField]
    private int _value = 0;
    [SerializeField]
    private int _startValue = 0;
    public int Value 
    { 
        get => _value; 
        set
        {
            _startValue = _displayedValue;
            _value = value;
            _startTime = Time.time;
        }
    }

    void Awake()
    {
        _label = GetComponent<TextMeshProUGUI>();
        _startTime = Time.time;
        _label.text = _value.ToString();
    }

    void Update()
    {
        if (_displayedValue == _value) { return; }
        
        float percent = Mathf.Clamp01((Time.time - _startTime) / Duration); 
        _displayedValue = Mathf.RoundToInt(Mathf.Lerp(_startValue, _value, percent));
        _label.text = _displayedValue.ToString();
    }

}
