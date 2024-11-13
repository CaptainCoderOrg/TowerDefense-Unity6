using UnityEngine;
using UnityEngine.Pool;

public class ParticleEffectsPool : MonoBehaviour 
{
    private static ParticleEffectsPool _instance;
    public static ParticleEffectsPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<ParticleEffectsPool>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }

    private IObjectPool<PoolableParticleSystem> _pool;
    [SerializeField]
    private PoolableParticleSystem _prefab;

    void Awake()
    {
        //public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        _pool = new ObjectPool<PoolableParticleSystem>(CreateParticles, OnTake, OnRelease);
    }

    private PoolableParticleSystem CreateParticles()
    {
        PoolableParticleSystem system = GameObject.Instantiate(_prefab, this.transform);
        system.Pool = _pool;
        return system;
    }

    internal void OnTake(PoolableParticleSystem system)
    {
        system.gameObject.SetActive(true);
    }

    internal void OnRelease(PoolableParticleSystem system)
    {
        system.gameObject.SetActive(false);
    }

    internal void SpawnDustParticles(Vector3 position)
    {
        PoolableParticleSystem spawned = _pool.Get();
        spawned.transform.position = position + new Vector3(0, .45f, 0);
    }
}