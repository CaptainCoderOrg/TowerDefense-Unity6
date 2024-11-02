using UnityEngine;
using UnityEngine.Events;

public class StructureController : MonoBehaviour
{
    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;
    public StructureController CloneInstance(Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(this, position, rotation);
    }

    
}