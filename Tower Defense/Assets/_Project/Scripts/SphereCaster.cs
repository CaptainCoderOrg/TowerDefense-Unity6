using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    public float Radius;
    public Color GizmoColor = Color.yellow;
    public LayerMask Layers;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawSphere(transform.position, Radius);
    }

    public Collider[] SphereCast() => Physics.OverlapSphere(transform.position, Radius, Layers);
}
