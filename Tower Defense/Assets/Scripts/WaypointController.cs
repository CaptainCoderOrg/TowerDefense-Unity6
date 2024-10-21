using UnityEditor;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public Color GizmoLineColor = Color.black;
    public float GizmoLineThickness = 2f;
    public WaypointController Next;

    public void OnDrawGizmos()
    {
        if (Next == null) { return; }
        Handles.color = GizmoLineColor;
        Handles.DrawLine(transform.position, Next.transform.position, GizmoLineThickness);
    }
}
