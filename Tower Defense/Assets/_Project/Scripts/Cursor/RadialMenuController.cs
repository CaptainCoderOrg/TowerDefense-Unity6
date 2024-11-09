using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public sealed class RadialMenuController : MonoBehaviour
{
    private CursorManagerController Cursor => CursorManagerController.Instance;
    public TileController Selected { get; private set; }
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
        if (_recentStructures.Count == 0) { return; }
        GameManagerController.Instance.OnMenuChanged.Invoke(gameObject);
        transform.position = tile.transform.position;
        gameObject.SetActive(true);
        Selected = tile;
        Cursor.Cursor.transform.position = tile.transform.position;
        Cursor.Cursor.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Cursor.Cursor.SetActive(false);
    }

    public void AddStructure(StructureData structureData)
    {
        _ = _recentStructures.Remove(structureData);
        _recentStructures.Insert(0, structureData);
        for (int i = 0; i < _recentStructures.Count && i < _buttons.Length; i++)
        {
            _buttons[i].Structure = _recentStructures[i];
        }
    }

}