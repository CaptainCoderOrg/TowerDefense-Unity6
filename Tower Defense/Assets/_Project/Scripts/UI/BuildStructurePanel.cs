using TMPro;
using UnityEngine;
using CaptainCoder.Unity.Panels;

public class BuildStructurePanel : MonoBehaviour
{
    private GameManagerController _gameManager;
    private PanelController _parent;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;

    void Awake()
    {
        _gameManager = GetComponentInParent<GameManagerController>();
        Debug.Assert(_gameManager != null, "StructurePanel could not find game manager");

        _parent = GetComponentInParent<PanelController>();
        Debug.Assert(_parent != null);
    }

    void OnEnable()
    {
        _gameManager.OnMenuChanged.AddListener(MenuChanged);
        _gameManager.OnGameWon.AddListener(Hide);
    }

    void OnDisable()
    {
        _gameManager.OnMenuChanged.RemoveListener(MenuChanged);
        _gameManager.OnGameWon.RemoveListener(Hide);
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
