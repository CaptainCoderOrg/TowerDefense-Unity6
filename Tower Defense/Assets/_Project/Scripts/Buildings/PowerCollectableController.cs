using UnityEngine;

public class PowerCollectableController : MonoBehaviour
{
    public int Value;
    private MouseEvents _events;

    void Awake()
    {
        _events = GetComponentInChildren<MouseEvents>();
        _events.OnClick.AddListener(Collect);
    }

    public void Collect()
    {
        GameManagerController.Instance.Money += Value;
        GameObject.Destroy(gameObject);
    }
}
