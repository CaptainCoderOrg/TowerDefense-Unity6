using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TurretController))]
public class TurretRotationController : MonoBehaviour
{
    private TurretController _controller;
    public float RotationSpeed = 1f;
    void Awake()
    {
        _controller = GetComponent<TurretController>();
        Debug.Assert(_controller != null, $"{nameof(TurretRotationController)} requires a {nameof(TurretController)}");
    }

    void Update()
    {
        if (_controller.Target == null) { return; }
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - _controller.Target.transform.position, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * RotationSpeed * Time.deltaTime);
    }

}
