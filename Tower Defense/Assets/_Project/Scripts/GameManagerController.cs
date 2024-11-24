using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using CaptainCoder.Unity.Extensions;

public class GameManagerController : MonoBehaviour
{
    [field: SerializeField]
    public Transform EnemiesContainer { get; private set; }
    [field: SerializeField]
    public Transform StructuresContainer { get; private set; }
    [field: SerializeField]
    public Transform CollectablesContainer { get; private set; }
    public GameStats Stats { get; private set; }
    [field: SerializeField]
    public UnityEvent OnGameWon { get; private set; }
    private HashSet<SpawnerController> _spawners = new();
    private HashSet<EnemyController> _enemies = new();
    [field: SerializeField]
    public int StartingEnergy { get; private set; } = 50;
    public bool IsRunning { get; set; } = false;
    public int Energy
    {
        get => EnergyText.Value;
    }
    public UnityEvent OnGameOver;
    public UnityEvent<GameObject> OnMenuChanged;
    public TextMeshProUGUI InfoText;
    // [field: SerializeField]
    // private List<StructureController> _structures = new();
    private HashSet<StructureController> _structures = new();
    public IEnumerable<StructureController> Structures => _structures.ToList();
    public IEnumerable<PowerCrystalController> PowerCrystals => _structures.GetComponents<PowerCrystalController>();
    [field: SerializeField]
    private AnimatedNumberText EnergyText { get; set; }

    void Awake()
    {
        EnemiesContainer = new GameObject("Enemies Container").transform;
        EnemiesContainer.SetParent(transform);
        StructuresContainer = new GameObject("Structures Container").transform;
        StructuresContainer.SetParent(transform);
        CollectablesContainer = new GameObject("Collectables Container").transform;
        CollectablesContainer.SetParent(transform);
        InfoText.text = "";
        EnergyText.Value = StartingEnergy;
        Stats = new() { StartTime = Time.time };
    }

    public void GameStart()
    {
        IsRunning = true;
    }

    public void RegisterSpawner(SpawnerController spawner)
    {
        _spawners.Add(spawner);
    }

    public void RegisterEnemy(EnemyController enemy)
    {
        _enemies.Add(enemy);
        Stats.EnemiesSpawned++;
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        if (_enemies.Remove(enemy) && IsGameWon())
        {
            GameWon();
        }
    }

    [Button("Game Won")]
    public void GameWon()
    {
        Stats.EndTime = Time.time;
        OnGameWon.Invoke();
    }

    public bool IsGameWon()
    {
        _spawners.RemoveWhere(s => s.IsDone);
        if (_spawners.Count > 0) { return false; }
        if (_enemies.Count > 0) { return false; }
        return true;
    }

    public void TriggerGameOver()
    {
        OnGameOver.Invoke();
    }

    public void TryAgain()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Title Screen");
    }


    public BuildResult TryPurchaseStructure(TileController controller, StructureData structure)
    {
        if (EnergyText.Value < structure.Price) { return BuildResult.Fail("Not enough energy"); }
        BuildResult result = controller.Build(structure);
        if (result.IsSuccess)
        {
            SpendEnergy(structure.Price);
        }
        return result;
    }


    public void AddStructure(StructureController structure) => _structures.Add(structure);
    public void RemoveStructure(StructureController structure) => _structures.Remove(structure);

    public void ShowRangesNear(TileController target, float distance = 3)
    {
        var structures = Structures.Where(s => Vector3.Distance(s.transform.position, target.transform.position) < distance);
        foreach (StructureController controller in structures)
        {
            controller.OnShowRange.Invoke();
        }
    }

    public void ShowAllRanges()
    {
        foreach (StructureController controller in Structures)
        {
            controller.OnShowRange.Invoke();
        }
    }

    public void HideAllRanges()
    {
        foreach (StructureController controller in Structures)
        {
            controller.OnHideRange.Invoke();
        }
    }

    public void CollectEnergy(int value)
    {
        Stats.EnergyCollected += value;
        EnergyText.Value += value;
    }

    public void SpendEnergy(int value) => EnergyText.Value -= value;

}