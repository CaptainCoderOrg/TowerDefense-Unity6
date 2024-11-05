using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseEvents : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnOver;
    public UnityEvent OnClick;
    public UnityEvent OnRightClick;
    private bool _isHovered;
    private void OnMouseEnter()
    {
        _isHovered = true;
        OnEnter.Invoke();
    }
    private void OnMouseExit()
    {
        _isHovered = false;
        OnExit.Invoke();
    }
    private void OnMouseOver() => OnOver?.Invoke();
    private void OnMouseUpAsButton()
    {
        if(EventSystem.current.IsPointerOverGameObject()) { return; }
        OnClick?.Invoke();
    }

    void Update()
    {
        if (_isHovered && Input.GetMouseButtonDown(1))
        {
            OnRightClick.Invoke();
        }
    }
}
