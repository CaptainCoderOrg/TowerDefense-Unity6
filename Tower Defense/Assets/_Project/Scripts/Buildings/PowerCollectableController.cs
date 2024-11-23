using UnityEngine;

public class PowerCollectableController : MonoBehaviour
{
    public int Value;
    private MouseEvents _events;
    private GameManagerController _gameManager;

    void Awake()
    {
        _events = GetComponentInChildren<MouseEvents>();
        _gameManager = GetComponentInParent<GameManagerController>();
        Debug.Assert(_gameManager != null, "Could not locate Game Manager");
    }

    void OnEnable()
    {
        _events.OnEnter.AddListener(Collect);
        _gameManager.OnGameWon.AddListener(Collect);
    }   

    void OnDisable()
    {
        _events.OnEnter.RemoveListener(Collect);
        _gameManager.OnGameWon.RemoveListener(Collect);
    }

    public void Collect()
    {
        FloatingNumbersPool.Instance.SpawnFloatingNumber(Value, transform.position);
        _gameManager.CollectEnergy(Value);
        GameObject.Destroy(gameObject);
    }
}
