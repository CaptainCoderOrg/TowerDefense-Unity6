using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabPanelController : MonoBehaviour
{
    private GameManagerController GameManager => GameManagerController.Instance;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public Animator Animator;
    public Button ToggleButton;
    public bool IsShowing;

    void Start()
    {
        if (GameManagerController.Instance != null)
        {
            GameManagerController.Instance?.OnMenuChanged.AddListener(MenuChanged);
            GameManagerController.Instance?.OnGameWon.AddListener(Hide);
            GameManagerController.Instance?.OnGameWon.AddListener(() => ToggleButton.enabled = false);
        }
    }

    private void MenuChanged(GameObject Menu)
    {
        if (Menu == this.gameObject) { return; }
        Hide();
    }

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
        if (GameManagerController.Instance != null)
        {
            GameManagerController.Instance.OnMenuChanged.Invoke(gameObject);
        }        
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
