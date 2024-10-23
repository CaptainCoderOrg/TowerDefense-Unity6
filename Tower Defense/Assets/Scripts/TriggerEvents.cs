using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<Collider> OnEnter { get; private set;}
    [field: SerializeField]
    public UnityEvent<Collider> OnExit { get; private set; }

    void OnTriggerEnter(Collider other) => OnEnter?.Invoke(other);
    void OnTriggerExit(Collider other) => OnExit?.Invoke(other);
}
