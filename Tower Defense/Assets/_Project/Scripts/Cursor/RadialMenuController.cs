using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public sealed class RadialMenuController : MonoBehaviour
{
    private GameManagerController _gameManager;
    [SerializeField]
    private Animator _animator;
    [field: SerializeField]
    public TextMeshProUGUI InfoText { get; private set; }
    private Coroutine _currentMessage;
    private CursorManagerController Cursor => CursorManagerController.Instance;
    public TileController Selected { get; private set; }
    [SerializeField]
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
        _gameManager = GetComponentInParent<GameManagerController>();
        Debug.Assert(_gameManager != null, $"Could not find {nameof(GameManagerController)}");
        _gameManager.OnGameWon.AddListener(() => this.gameObject.SetActive(false));
        InfoText.text = string.Empty;
        _buttons = GetComponentsInChildren<RadialMenuButtonController>();
        foreach (RadialMenuButtonController b in _buttons)
        {
            b.gameObject.SetActive(false);
        }
        _gameManager.OnMenuChanged.AddListener(MenuChanged);
        SynchronizeButtons();
        Hide();
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

    public void Preload()
    {
        Show(FindFirstObjectByType<TileController>());
    }

    public void Show(TileController tile)
    {
        if (Selected == tile) 
        { 
            Hide();
            return;
        }
        if (_recentStructures.Count == 0) { return; }
        InfoText.text = string.Empty;
        _gameManager.OnMenuChanged.Invoke(gameObject);
        transform.position = Camera.main.WorldToScreenPoint(tile.transform.position);
        gameObject.SetActive(true);
        Selected = tile;
        Cursor.Cursor.transform.position = tile.transform.position;
        Cursor.Cursor.SetActive(true);
        _gameManager.HideAllRanges();
        _gameManager.ShowRangesNear(Selected);
        _animator.SetTrigger("Show");
    }

    public void Hide()
    {
        Selected = null;
        InfoText.text = string.Empty;
        gameObject.SetActive(false);
        Cursor.Cursor.SetActive(false);
        _gameManager.HideAllRanges();
        foreach (RadialMenuButtonController button in _buttons)
        {
            button.Clear();
        }
    }

    public void AddStructure(StructureData structureData)
    {
        _ = _recentStructures.Remove(structureData);
        _recentStructures.Insert(0, structureData);
        SynchronizeButtons();
    }

    private void SynchronizeButtons()
    {
        for (int i = 0; i < _recentStructures.Count && i < _buttons.Length; i++)
        {
            _buttons[i].Structure = _recentStructures[i];
        }
    }

    internal void Clear()
    {
        foreach (var button in _buttons) { button.Clear(); }
    }
}