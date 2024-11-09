using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public sealed class RadialMenuController : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI InfoText { get; private set; }
    private Coroutine _currentMessage;
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
        InfoText.text = string.Empty;
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

    public void ShowMessage(string message, float delay = -1)
    {
        InfoText.text = message;
        if (_currentMessage != null) { StopCoroutine(_currentMessage); }
        if (delay > 0) {
            _currentMessage = StartCoroutine(HideMessage(delay));
        }
    }

    private IEnumerator HideMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        InfoText.text = string.Empty;
    }

    private void MenuChanged(GameObject Menu)
    {
        if (Menu == this.gameObject) { return; }
        Hide();
    }

    public void Show(TileController tile)
    {
        if (_recentStructures.Count == 0) { return; }
        InfoText.text = string.Empty;
        GameManagerController.Instance.OnMenuChanged.Invoke(gameObject);
        transform.position = Camera.main.WorldToScreenPoint(tile.transform.position);
        gameObject.SetActive(true);
        Selected = tile;
        Cursor.Cursor.transform.position = tile.transform.position;
        Cursor.Cursor.SetActive(true);
    }

    public void Hide()
    {
        InfoText.text = string.Empty;
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