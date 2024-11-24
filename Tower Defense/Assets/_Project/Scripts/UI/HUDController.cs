using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Animator))]
public class HUDController : MonoBehaviour
{
    private GameManagerController _gameManager;
    private Animator _animator;

    void Awake()
    {
        _gameManager = GetComponentInParent<GameManagerController>();
        Debug.Assert(_gameManager != null, $"Could not find {nameof(GameManagerController)}");
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator != null);
    }

    void OnEnable()
    {
        _gameManager.OnGameOver.AddListener(YouDied);
    }

    void OnDisable()
    {
        _gameManager.OnGameOver.RemoveListener(YouDied);
    }

    public void YouDied() => _animator.SetTrigger("OnGameOver");
}
