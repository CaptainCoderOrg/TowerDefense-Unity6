using UnityEngine;

public class TileCanvasController : MonoBehaviour
{
    private static TileCanvasController _instance;
    public TileController Selected;
    public GameObject Cursor;
    public GameObject BuildMenu;
    public TurretMenuController TurretMenu;
    public PowerCrystalMenuController CrystalMenu;
    public static TileCanvasController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<TileCanvasController>();
                Debug.Assert(_instance != null, $"Unable to find {nameof(TileCanvasController)}");
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
        TurretMenu = GetComponentInChildren<TurretMenuController>(true);
        Debug.Assert(TurretMenu != null, "Could not find TurretMenu");
        CrystalMenu = GetComponentInChildren<PowerCrystalMenuController>(true);
        Debug.Assert(CrystalMenu != null, "Could not find CrystalMenu");
    }

    public void SelectTile(TileController tile)
    {
        Cursor.transform.position = tile.transform.position;
        Cursor.SetActive(true);
        Selected = tile;
        if (tile.Structure == null)
        {
            BuildMenu.transform.position = tile.transform.position;
            BuildMenu.SetActive(true);
        }
        else
        {
            Selected.Structure.OnSelected.Invoke();
        }
    }

    public void ClearSelection()
    {
        Selected.Structure?.OnDeselected.Invoke();
        Selected = null;
        Cursor.SetActive(false);
        BuildMenu.SetActive(false);
    }

}