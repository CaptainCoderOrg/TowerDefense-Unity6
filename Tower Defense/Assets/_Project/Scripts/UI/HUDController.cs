using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HUDController : MonoBehaviour
{
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
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator != null);
        GameManagerController.Instance.OnGameOver.AddListener(YouDied);
    }

    public void YouDied() => _animator.SetTrigger("OnGameOver");

    internal void SpawnFloatingNumber(int value, Vector3 position)
    {
        FloatingNumberController spawned = GameObject.Instantiate(_floatingNumberPrefab, this.transform);
        spawned.Value = value;
        spawned.transform.position = Camera.main.WorldToScreenPoint(position);
    }
}
