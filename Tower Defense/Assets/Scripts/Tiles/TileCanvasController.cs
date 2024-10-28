using UnityEngine;

public class TileCanvasController : MonoBehaviour
{
    private static TileCanvasController _instance;
    public TileController Selected;
    public GameObject Cursor;
    public GameObject BuildMenu;
    public GameObject TurretMenu;
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

    public void SelectTile(TileController tile)
    {
        Cursor.transform.position = tile.transform.position;
        Cursor.SetActive(true);
        Selected = tile;
        if (tile.Turret == null)
        {
            BuildMenu.transform.position = tile.transform.position;
            BuildMenu.SetActive(true);
        }
        else
        {
            TurretMenu.transform.position = tile.transform.position;
            TurretMenu.SetActive(true);
        }
    }

    public void ClearSelection()
    {
        Selected = null;
        Cursor.SetActive(false);
        BuildMenu.SetActive(false);
        TurretMenu.SetActive(false);
    }

    public void RemoveTurret()
    {
        if (Selected == null) { return; }
        Selected.RemoveTurret();
        ClearSelection();
    }

}