using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private float _currentDangle = 0;
    [SerializeField]
    private int _angle = 0;
    public int Angle 
    { 
        get => _angle; 
        private set
        {
            StopAllCoroutines();
            StartCoroutine(Rotate(_currentDangle, value));
            _angle = value % 360;
        }
    }
    [field: SerializeField]
    public float Duration { get; set; } = .5f;
    [field: SerializeField]
    public AnimationCurve RotationCurve { get; set; }

    private IEnumerator Rotate(float startAngle, int endAngle)
    {
        float startTime = Time.time;
        float percent = 0;
        while (percent < 1)
        {
            percent = Mathf.Clamp01((Time.time - startTime) / Duration);
            percent = RotationCurve.Evaluate(percent);
            _currentDangle = Mathf.Lerp(startAngle, endAngle, percent);
            _currentDangle = _currentDangle % 360;
            transform.rotation = transform.rotation.WithY(_currentDangle);
            yield return null;
        }
        transform.rotation = transform.rotation.WithY(endAngle);
    }

    public void RotateRight(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed) { RotateRight(); }
    }

    [Button("RotateRight")]
    public void RotateRight()
    {
        Angle += 90;
    }

    public void RotateLeft(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed) { RotateLeft(); }
    }

    [Button("RotateLeft")]
    public void RotateLeft()
    {
        Angle -= 90;
    }

}