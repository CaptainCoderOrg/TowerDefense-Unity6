using UnityEngine;
using UnityEngine.Events;

public class TileCanvasController : MonoBehaviour
{
    private static TileCanvasController _instance;
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


}