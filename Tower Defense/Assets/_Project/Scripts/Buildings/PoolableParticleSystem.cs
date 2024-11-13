using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ParticleSystem))]
public class PoolableParticleSystem : MonoBehaviour
{
    public ParticleSystem System { get; private set; }
    public IObjectPool<PoolableParticleSystem> Pool { get; set; }

    void Start()
    {
        System = GetComponent<ParticleSystem>();
        var main = System.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        Pool.Release(this);
    }
}