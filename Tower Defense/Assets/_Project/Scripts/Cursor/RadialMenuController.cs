using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public sealed class RadialMenuController : MonoBehaviour
{
    private TileController _selected;
    private List<StructureData> _recentStructures = new();
    private RadialMenuButtonController[] _buttons;
    private static RadialMenuController _instance;
    public static RadialMenuController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<RadialMenuController>(FindObjectsInactive.Include);
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }

    void Awake()
    {
        _buttons = GetComponentsInChildren<RadialMenuButtonController>();
        foreach (RadialMenuButtonController b in _buttons)
        {
            b.gameObject.SetActive(false);
        }
        GameManagerController.Instance.OnMenuChanged.AddListener(MenuChanged);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Hide();
        }
    }

    private void MenuChanged(GameObject Menu)
    {
        if (Menu == this.gameObject) { return; }
        Hide();
    }

    public void Show(TileController tile)
    {
        GameManagerController.Instance.OnMenuChanged.Invoke(gameObject);
        transform.position = tile.transform.position;
        gameObject.SetActive(true);
        _selected = tile;
    }

    public void Hide() => gameObject.SetActive(false);

    public void ClickButton(StructureData structureData)
    {
        if (_selected == null) { return; }
        if (GameManagerController.Instance.TryPurchaseStructure(_selected, structureData))
        {
            Hide();
        }
        else
        {
            Debug.Log("Could not purchase at location");
        }
        
    }

    public void AddStructure(StructureData structureData)
    {
        _ = _recentStructures.Remove(structureData);
        _recentStructures.Insert(0, structureData);
        for (int i = 0; i < _recentStructures.Count && i < _buttons.Length; i++)
        {
            RadialMenuButtonController b = _buttons[i];
            b.gameObject.SetActive(true);
            b.Button.onClick.RemoveAllListeners();
            StructureData structure = _recentStructures[i];
            b.Icon.texture = structure.Icon;
            b.Button.onClick.AddListener(() => ClickButton(structure));
        }
    }

}