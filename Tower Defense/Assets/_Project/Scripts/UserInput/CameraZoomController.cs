using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoomController : MonoBehaviour
{   
    [field: SerializeField]
    public float MinZoom { get; private set; } = 3;
    [field: SerializeField]
    public float MaxZoom { get; private set; } = 10;
    [field: SerializeField]
    public float ZoomScale { get; private set; } = .2f;
    [field: SerializeField]
    public float Speed { get; private set; } = 0.5f;
    [SerializeField]
    private float _differenceEpsilon = .01f;

    [SerializeField]
    private float _zoomLevel;
    public float ZoomLevel 
    { 
        get => _zoomLevel; 
        private set
        {
            _zoomLevel = Mathf.Clamp(value, MinZoom, MaxZoom);
        }
    }

    void Awake()
    {
        ZoomLevel = Camera.main.orthographicSize;
    }

    void Update()
    {
        float currentSize = Camera.main.orthographicSize;
        float difference = currentSize - ZoomLevel;
        if (Mathf.Abs(difference) > _differenceEpsilon)
        {
            Camera.main.orthographicSize -= difference * Speed * Time.deltaTime;
        } 
        else
        {
            Camera.main.orthographicSize = ZoomLevel;
        }
    }

    public void Zoom(float amount)
    {
        amount = amount == 0 ? 0 : Mathf.Sign(amount);
        ZoomLevel -= amount * ZoomScale;
    }
    
    public void Zoom(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed) 
        { 
            Zoom(callbackContext.ReadValue<float>());
        }
    }
}