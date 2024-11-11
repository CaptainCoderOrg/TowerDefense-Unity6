using UnityEngine;

public class WaypointTraveler : MonoBehaviour
{
    public WaypointController Target;
    public float Speed = 1.0f;
    public float delta = 0.01f;
    public Transform[] RotateToFaceWaypoint;
    void Update()
    {
        if (Target == null) { return; }
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        float distance = (Target.transform.position - transform.position).magnitude;
        if (Mathf.Abs(distance) < delta)
        {
            Target = Target.Next;
        }
        if (Target != null)
        {
            foreach (Transform tr in RotateToFaceWaypoint)
            {
                Quaternion targetLook = Quaternion.LookRotation(Target.transform.position - tr.position);
                Quaternion rotation = tr.rotation;
                rotation.y = targetLook.y;
                tr.rotation = rotation;
            }
        }
    }

    public float CalculateDistanceToFinalWaypoint()
    {
        if (Target == null) { return 0; }
        return (transform.position - Target.transform.position).magnitude + Target.Distance;
    }
}