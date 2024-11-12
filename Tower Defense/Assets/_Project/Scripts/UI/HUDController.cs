using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Animator))]
public class HUDController : MonoBehaviour
{
    private IObjectPool<FloatingNumberController> _floatingNumbersPool;
    [SerializeField]
    private FloatingNumberController _floatingNumberPrefab;
    private static HUDController _instance;
    public static HUDController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<HUDController>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }

    private Animator _animator;

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
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator != null);
        GameManagerController.Instance.OnGameOver.AddListener(YouDied);
    }

    private FloatingNumberController CreateFloatingNumber()
    {
        FloatingNumberController spawned = GameObject.Instantiate(_floatingNumberPrefab, this.transform);
        spawned.Reclaim = Release;
        return spawned;
    }

    public void YouDied() => _animator.SetTrigger("OnGameOver");

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
