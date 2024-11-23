using TMPro;
using UnityEngine;
using CaptainCoder.Unity.Panels;

public class BuildStructurePanel : MonoBehaviour
{
    private GameManagerController GameManager => GameManagerController.Instance;
    private PanelController _parent;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;

    void Awake()
    {
        _parent = GetComponentInParent<PanelController>();
        Debug.Assert(_parent != null);
    }

    void Start()
    {
        if (GameManagerController.Instance != null)
        {
            GameManagerController.Instance?.OnMenuChanged.AddListener(MenuChanged);
            GameManagerController.Instance?.OnGameWon.AddListener(Hide);
        }
    }

    private void MenuChanged(GameObject Menu)
    {
        if (Menu == this.gameObject) { return; }
        Hide();
    }

    public void Toggle() => _parent.Toggle();
    public void Show() => _parent.Show();
    public void Hide() => _parent.Hide();
}
