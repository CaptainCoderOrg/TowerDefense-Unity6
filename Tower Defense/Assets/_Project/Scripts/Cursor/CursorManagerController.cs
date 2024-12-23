using UnityEngine;
using UnityEngine.Events;

public class CursorManagerController : MonoBehaviour
{
    public GameObject Cursor;
    public UnityEvent<TileController> OnSelectTile;
    private GameManagerController _gameManager;
    private TileController _selected;
    public TileController Selected
    {
        get => _selected;
        private set
        {
            _selected = value;
            OnSelectTile.Invoke(_selected);
        }
    }
    private static CursorManagerController _instance;
    public static CursorManagerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<CursorManagerController>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }
    private ICursorBehaviour _cursorBehaviour;
    public ICursorBehaviour CursorBehaviour
    {
        get => _cursorBehaviour;
        private set
        {
            _cursorBehaviour?.Deinitialize();
            if (Selected != null)
            {
                _cursorBehaviour?.ExitTile(Selected);
            }
            _cursorBehaviour = value;
            _cursorBehaviour.Initialize(_gameManager);
            ClearSelection();
        }
    }

    void Awake()
    {
        _gameManager = GetComponentInParent<GameManagerController>();
        Debug.Assert(_gameManager != null, "Could not find GameManager"); 
        CursorBehaviour = new DefaultCursorBehaviour();
        _gameManager.OnGameWon.AddListener(() => CursorBehaviour = new DisabledCursorBehaviour());
    }


    public void StartBuildMode(StructureData data)
    {
        CursorBehaviour = new BuildCursorBehaviour(data);
        
    }
    public void DefaultMode() => CursorBehaviour = new DefaultCursorBehaviour();
    public void EnterTile(TileController controller) => CursorBehaviour.EnterTile(controller);
    public void ExitTile(TileController controller) => CursorBehaviour.ExitTile(controller);
    public void ClickTile(TileController controller) => CursorBehaviour.ClickTile(controller);
    public void RightClickTile(TileController controller) => CursorBehaviour.RightClickTile(controller); 

    public void SelectTile(TileController tile)
    {
        if (tile.Structure == null) { return; }
        Cursor.transform.position = tile.transform.position;
        Cursor.SetActive(true);
        Selected = tile;
        Selected.Structure.OnSelected.Invoke();
    }

    public void ClearSelection()
    {
        Selected?.Structure?.OnDeselected.Invoke();
        Selected = null;
        Cursor.SetActive(false);
    }
}
