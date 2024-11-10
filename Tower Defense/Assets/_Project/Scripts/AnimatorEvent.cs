using UnityEngine;
using UnityEngine.Events;

public class AnimatorEvent : MonoBehaviour
{
    public UnityEvent OnTrigger;
    public void TriggerEvent()
    {
        OnTrigger.Invoke();
    }
}