using UnityEngine;

public class TileCanvasController : MonoBehaviour
{
    private static TileCanvasController _instance;
    public TileController Selected;
    public GameObject Cursor;
    public GameObject BuildMenu;
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
        if (tile.Turret != null) { return; }
        Cursor.transform.position = tile.transform.position;
        Cursor.SetActive(true);
        BuildMenu.transform.position = tile.transform.position;
        BuildMenu.SetActive(true);
        Selected = tile;
    }

    public void ClearSelection()
    {
        Selected = null;
        Cursor.SetActive(false);
        BuildMenu.SetActive(false);
    }

}