using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class TabPanelController : MonoBehaviour
{
    private static TabPanelController _instance;
    public static TabPanelController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<TabPanelController>();
                Debug.Assert(_instance != null, $"Unable to find {nameof(TabPanelController)}");
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
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
