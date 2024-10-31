using UnityEngine;

public class TileCanvasController : MonoBehaviour
{
    private static TileCanvasController _instance;
    public TileController Selected;
    public GameObject Cursor;
    public GameObject BuildMenu;
    public TurretMenuController _turretMenu;
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
        _turretMenu = GetComponentInChildren<TurretMenuController>(true);
        Debug.Assert(_turretMenu != null, "Could not find TurretMenu");
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
        else if (Selected.Structure.GetComponent<TurretController>() is TurretController turret)
        {
            turret.AoE.SetVisibility(true);
            _turretMenu.transform.position = tile.transform.position;
            _turretMenu.Show();
        }
    }

    public void ClearSelection()
    {
        Selected.Structure?.GetComponent<TurretController>()?.AoE.SetVisibility(false);
        Selected = null;
        Cursor.SetActive(false);
        BuildMenu.SetActive(false);
        _turretMenu.Hide();
    }

}