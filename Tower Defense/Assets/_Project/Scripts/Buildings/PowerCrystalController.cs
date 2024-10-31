using UnityEngine;

[RequireComponent(typeof(CooldownController))]
public class PowerCrystalController : MonoBehaviour
{
    [field: SerializeField]
    public CooldownController Cooldown { get; private set; }
    public PowerCollectableController CollectablePrefab;
    public Transform[] SpawnPositions;
    public bool IsDecoration;
    

    void Awake()
    {
        if (IsDecoration) { return; }
        Cooldown = GetComponent<CooldownController>();
        Cooldown.OnCooldownFinished.AddListener(CreatePowerCollectable);
    }

    public void CreatePowerCollectable()
    {
        Transform spawnAt = SpawnPositions.SelectRandomOrDefault(transform);
        GameObject.Instantiate(CollectablePrefab, spawnAt.position, spawnAt.rotation);
        Cooldown.StartCooldown();
    }

}
