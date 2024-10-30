using UnityEditor;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public Color GizmoLineColor = Color.black;
    public float GizmoLineThickness = 2f;
    public WaypointController Next;
    public float Distance;

    void Awake()
    {
        Distance = CalculateDistanceToEnd();
    }

    public float CalculateDistanceToEnd()
    {
        if (Next == null) { return 0; }
        float distance = (transform.position - Next.transform.position).magnitude + Next.CalculateDistanceToEnd();
        return distance;
    }

    public void OnDrawGizmos()
    {
        if (Next == null) { return; }
        Handles.color = GizmoLineColor;
        Handles.DrawLine(transform.position, Next.transform.position, GizmoLineThickness);
    }
}
