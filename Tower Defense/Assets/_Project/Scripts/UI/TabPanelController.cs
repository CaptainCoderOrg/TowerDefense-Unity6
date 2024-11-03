using NaughtyAttributes;
using UnityEngine;

public class TabPanelController : MonoBehaviour
{
    public Animator Animator;
    public bool IsShowing;

    [Button("Toggle")]
    public void Toggle()
    {
        if (IsShowing) 
        { 
            Hide(); 
        }
        else
        {
            Show();
        }

    }

    [Button("Show")]
    public void Show()
    {
        if (IsShowing) { return; }
        IsShowing = true;
        Animator.SetTrigger("Show");
    }

    [Button("Hide")]
    public void Hide()
    {
        if (!IsShowing) { return; }
        IsShowing = false;
        Animator.SetTrigger("Hide");
    }
}
