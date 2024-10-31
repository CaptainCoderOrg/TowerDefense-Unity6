using UnityEngine;

public class StructureController : MonoBehaviour
{
    public StructureController CloneInstance(Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(this, position, rotation);
    }
}