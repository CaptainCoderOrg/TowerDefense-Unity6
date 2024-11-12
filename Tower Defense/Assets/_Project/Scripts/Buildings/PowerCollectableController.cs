using UnityEngine;

public class PowerCollectableController : MonoBehaviour
{
    public int Value;
    private MouseEvents _events;

    void Awake()
    {
        _events = GetComponentInChildren<MouseEvents>();
        _events.OnEnter.AddListener(Collect);
    }

    public void Collect()
    {
        FloatingNumbersPool.Instance.SpawnFloatingNumber(Value, transform.position);
        GameManagerController.Instance.Money += Value;
        GameObject.Destroy(gameObject);
    }
}
