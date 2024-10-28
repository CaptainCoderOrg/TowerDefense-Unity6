using UnityEngine;

public class TurretAoEController : MonoBehaviour
{
    [field: SerializeField]
    public float Range { get; private set;} = 3.0f;
    public MeshRenderer AoERenderer;
    public void SetVisibility(bool isVisible) => AoERenderer.enabled = isVisible;

    void Awake()
    {
        SetRange(Range);
    }

    public void SetRange(float range)
    {
        Range = range;
        Vector3 localScale = AoERenderer.transform.localScale;
        localScale.x = range;
        localScale.z = range;
        AoERenderer.transform.localScale = localScale;
    }
}
