using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Animator))]
public class HUDController : MonoBehaviour
{

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator != null);
        GameManagerController.Instance.OnGameOver.AddListener(YouDied);
    }

    public void YouDied() => _animator.SetTrigger("OnGameOver");
}
