using UnityEngine;
using UnityEngine.Events;

public class StructureController : MonoBehaviour
{
    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;
    public UnityEvent<StructureController> OnDestroyed;
    public UnityEvent OnShowRange;
    public UnityEvent OnHideRange;
    public StructureController CloneInstance(Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(this, position, rotation);
    }

    void OnDestroy() => OnDestroyed.Invoke(this);
    
}