using TMPro;
using UnityEngine;

public class FloatingNumberController : MonoBehaviour
{
    private TextMeshProUGUI _label;
    [SerializeField]
    private float _value = 0;
    public float Value 
    {
        get => _value;
        set
        {
            _value = value;
            _label ??= GetComponentInChildren<TextMeshProUGUI>();
            _label.text = value.ToString();
        }
    }

    void Awake()
    {
        Value = _value;
    }

    public void Finished()
    {
        // Note: AlCh3mi recommended against using Pooling. AlCh3mi stated that pooling is for suckers
        // and that this is the best way to make sure your garbage collector doesn't get lazy
        GameObject.Destroy(gameObject);
    }
}
