using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private int _angle = 0;
    public int Angle 
    { 
        get => _angle; 
        private set
        {
            StopAllCoroutines();
            StartCoroutine(Rotate(_angle, value));
            _angle = value % 360;
        }
    }
    [field: SerializeField]
    public float Duration { get; set; } = .5f;

    private IEnumerator Rotate(int startAngle, int endAngle)
    {
        float startTime = Time.time;
        float percent = 0;
        while (percent < 1)
        {
            percent = Mathf.Clamp01((Time.time - startTime) / Duration);
            float currentAngle = Mathf.Lerp(startAngle, endAngle, percent);
            transform.rotation = GetQuaternion(currentAngle);
            yield return null;
        }
        transform.rotation = GetQuaternion(endAngle);
    }
    private Quaternion GetQuaternion(float angle) =>  
        Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);

    [Button("RotateRight")]
    public void RotateRight()
    {
        Angle += 90;
    }

    [Button("RotateLeft")]
    public void RotateLeft()
    {
        Angle -= 90;
    }

}