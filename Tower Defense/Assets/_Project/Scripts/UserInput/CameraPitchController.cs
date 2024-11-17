using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPitchController : MonoBehaviour
{
    [SerializeField]
    private float[] CameraPitches = { 22.5f, 30f };
    private int _pitchIx = 0;
    private float _current;

    [SerializeField]
    private Transform _targetCamera;
    
    [field: SerializeField]
    public float Duration { get; set; } = .5f;
    [field: SerializeField]
    public AnimationCurve RotationCurve { get; set; }

    void Awake()
    {
        _current = CameraPitches[_pitchIx];
    }

    private IEnumerator Rotate(float startAngle, float endAngle)
    {
        float startTime = Time.time;
        float percent = 0;
        while (percent < 1)
        {
            percent = Mathf.Clamp01((Time.time - startTime) / Duration);
            percent = RotationCurve.Evaluate(percent);
            _current = Mathf.Lerp(startAngle, endAngle, percent);
            _current = _current % 360;
            _targetCamera.rotation = _targetCamera.rotation.WithX(_current);
            yield return null;
        }
        _targetCamera.rotation = _targetCamera.rotation.WithX(endAngle);
    }

    [Button("Tilt")]
    public void Tilt()
    {
        _pitchIx = (_pitchIx + 1) % CameraPitches.Length;
        StopAllCoroutines();
        StartCoroutine(Rotate(_current, CameraPitches[_pitchIx]));
    }
    
    public void Tilt(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed) { Tilt(); }
    }
}