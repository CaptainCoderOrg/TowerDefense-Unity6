using UnityEngine;
using UnityEngine.Events;

public class MouseEvents : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnOver;

    private void OnMouseEnter() => OnEnter?.Invoke();
    private void OnMouseExit() => OnExit?.Invoke();
    private void OnMouseOver() => OnOver?.Invoke();
}
