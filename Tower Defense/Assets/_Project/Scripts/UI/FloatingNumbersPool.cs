using UnityEngine;
using UnityEngine.Pool;

public class FloatingNumbersPool : MonoBehaviour 
{
    private static FloatingNumbersPool _instance;
    public static FloatingNumbersPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<FloatingNumbersPool>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }

    private IObjectPool<FloatingNumberController> _floatingNumbersPool;
    [SerializeField]
    private FloatingNumberController _floatingNumberPrefab;

    void Awake()
    {
        /* 
        public ObjectPool(
            Func<T> createFunc, 
            Action<T> actionOnGet = null, 
            Action<T> actionOnRelease = null, 
            Action<T> actionOnDestroy = null, 
            bool collectionCheck = true, 
            int defaultCapacity = 10,
            int maxSize = 10000)
        */
        _floatingNumbersPool = new ObjectPool<FloatingNumberController>(CreateFloatingNumber);
    }

    private FloatingNumberController CreateFloatingNumber()
    {
        FloatingNumberController spawned = GameObject.Instantiate(_floatingNumberPrefab, this.transform);
        spawned.Reclaim = Release;
        return spawned;
    }

    internal void SpawnFloatingNumber(int value, Vector3 position)
    {
        FloatingNumberController spawned = _floatingNumbersPool.Get();
        spawned.Value = value;
        spawned.transform.position = Camera.main.WorldToScreenPoint(position);
        spawned.gameObject.SetActive(true);
    }

    
    internal void Release(FloatingNumberController controller)
    {
        controller.gameObject.SetActive(false);
        _floatingNumbersPool.Release(controller);
    }
}