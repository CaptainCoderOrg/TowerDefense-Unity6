using UnityEngine;

public class TurretAoEController : MonoBehaviour
{
    [SerializeField]
    private float _range = 3.0f;
    public int UpgradePrice => 25 * (int)Mathf.Round(_range);
    public float Range 
    { 
        get => _range;
        set => SetRange(value);
    }
    public MeshRenderer AoERenderer;
    public void SetVisibility(bool isVisible) => AoERenderer.enabled = isVisible;

    void Awake()
    {
        SetRange(Range);
    }

    void OnValidate()
    {
        SetRange(_range);
    }

    public void SetRange(float range)
    {
        _range = range;
        Vector3 localScale = AoERenderer.transform.localScale;
        localScale.x = range;
        localScale.z = range;
        AoERenderer.transform.localScale = localScale;
    }
}
