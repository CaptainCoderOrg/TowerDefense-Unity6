using UnityEngine;

public class FaceCameraController : MonoBehaviour
{
    void Update()
    {
        float angle = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
