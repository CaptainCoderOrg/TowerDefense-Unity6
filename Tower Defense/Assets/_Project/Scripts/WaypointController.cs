#if UNITY_EDITOR
using UnityEditor;
#endif
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
    #if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (Next == null) { return; }
        Handles.color = GizmoLineColor;
        Handles.DrawLine(transform.position, Next.transform.position, GizmoLineThickness);
    }
    #endif
}
