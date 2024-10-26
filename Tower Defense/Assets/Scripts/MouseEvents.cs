using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseEvents : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnOver;
    public UnityEvent OnClick;

    private void OnMouseEnter() => OnEnter?.Invoke();
    private void OnMouseExit() => OnExit?.Invoke();
    private void OnMouseOver() => OnOver?.Invoke();
    private void OnMouseUpAsButton()
    {
        if(EventSystem.current.IsPointerOverGameObject()) { return; }
        OnClick?.Invoke();
    }
}
