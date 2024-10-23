using UnityEngine;

public class TurretController : MonoBehaviour
{
    public EnemyController Target;
    public float RotationSpeed = 1f;
    void Awake()
    {
        TriggerEvents triggers = GetComponentInChildren<TriggerEvents>();
        Debug.Assert(triggers != null, "No TriggerEvents found on children");
        triggers.OnEnter.AddListener(HandleTriggerEntered);
        triggers.OnExit.AddListener(HandleTriggerExited);
    }

    void Update()
    {
        if (Target == null) { return; } 
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - Target.transform.position, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * RotationSpeed * Time.deltaTime);
    }

    public void HandleTriggerEntered(Collider other)
    {
        Target = other.attachedRigidbody.GetComponent<EnemyController>();
    }

    public void HandleTriggerExited(Collider other)
    {
        EnemyController exited = other.attachedRigidbody.GetComponent<EnemyController>();
        if (exited == Target) 
        { 
            Target = null; 
        }
    }
}
