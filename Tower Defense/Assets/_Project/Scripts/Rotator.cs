using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RotationSpeed = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, RotationSpeed * 360 * Time.deltaTime, 0);
    }
}
