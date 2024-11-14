using UnityEngine;
using UnityEngine.Events;

public class CooldownController : MonoBehaviour
{
    [Tooltip("The total Cooldown time")]
    public float Cooldown = 5;
    [Tooltip("The amount of time until the Cooldown is finished.")]
    public float IterationCooldown;
    public float CooldownPercent => 1 - (IterationCooldown / Cooldown);
    public UnityEvent OnCooldownFinished;
    public UnityEvent OnCooldownStart;
    public UnityEvent<CooldownController> OnCooldownTick;
    private bool IsCoolingDown => IterationCooldown > 0;
    public bool StartOnStart = false;

    void Start()
    {
        if (StartOnStart)
        {
            StartCooldown();
        }
    }

    void Update()
    {
        if (!IsCoolingDown) { return; } 
        IterationCooldown -= Time.deltaTime;
        OnCooldownTick.Invoke(this);
        if (IterationCooldown <= 0)
        {
            OnCooldownFinished.Invoke();
        }
    }

    public void StartCooldown()
    {
        IterationCooldown = Cooldown;
        OnCooldownStart.Invoke();
    }
}