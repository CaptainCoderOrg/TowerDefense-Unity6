using UnityEngine;

public class WaypointTraveler : MonoBehaviour
{
    public WaypointController Target;
    public float Speed = 1.0f;
    public float delta = 0.01f;
    void Update()
    {
        if (Target == null) { return; }
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        float distance = (Target.transform.position - transform.position).magnitude;
        if (Mathf.Abs(distance) < delta)
        {
            Target = Target.Next;
        }
    }
}
