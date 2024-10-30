using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEventMaskController : MonoBehaviour
{
    public LayerMask EventMask;
    
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        camera.eventMask = EventMask;
    }
}
