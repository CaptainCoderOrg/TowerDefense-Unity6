using System;
using UnityEngine;
using UnityEngine.Events;

public class StructureController : MonoBehaviour
{
    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;
    public UnityEvent<StructureController> OnDestroyed;
    public UnityEvent OnShowRange;
    public UnityEvent OnHideRange;
    public UnityEvent OnSpawn;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public StructureController CloneInstance(Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(this, position, rotation);
    }

    internal void DefaultSpawnAnimation()
    {
        _animator?.SetTrigger("Spawn");
    }

    void OnDestroy() => OnDestroyed.Invoke(this);
    
}