using UnityEngine;

public class PowerCollectableController : MonoBehaviour
{
    public int Value;
    private MouseEvents _events;

    void Awake()
    {
        _events = GetComponentInChildren<MouseEvents>();
        _events.OnEnter.AddListener(Collect);
        GameManagerController.Instance.OnGameWon.AddListener(Collect);
    }

    public void Collect()
    {
        FloatingNumbersPool.Instance.SpawnFloatingNumber(Value, transform.position);
        GameManagerController.Instance.Energy += Value;
        GameObject.Destroy(gameObject);
    }
}
