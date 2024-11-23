using UnityEngine;

public sealed class PanelController : MonoBehaviour 
{
    private Animator _animator;
    public bool IsShowing { get; private set; }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Show()
    {
        IsShowing = true;
        _animator.SetTrigger("Show");
    }

    public void Hide()
    {
        IsShowing = false;
        _animator.SetTrigger("Hide");
    }
}