using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    public UnityEvent OnGameOver;
    public UnityEvent<GameObject> OnMenuChanged;
    public TextMeshProUGUI InfoText;
    // [field: SerializeField]
    // private List<StructureController> _structures = new();
    private HashSet<StructureController> _structures = new();
    public IEnumerable<StructureController> Structures => _structures.ToList();
    public IEnumerable<PowerCrystalController> PowerCrystals => _structures.GetComponents<PowerCrystalController>();
    private static GameManagerController _instance;
    public static GameManagerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<GameManagerController>();
            }
            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        _instance = null;
    }

    public TextMeshProUGUI MoneyText;

    [SerializeField]
    private int _money = 500;
    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            UpdateMoney();
        }
    }

    [Button("Update Money")]
    public void UpdateMoney()
    {
        MoneyText.text = Money.ToString();
    }

    void Awake()
    {
        InfoText.text = "";
        UpdateMoney();
    }

    void OnValidate()
    {
        UpdateMoney();
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
        if (Money < structure.Price) { return BuildResult.Fail("Not enough energy"); }
        if (!controller.Build(structure)) { return BuildResult.Fail("Cannot place here"); }
        Money -= structure.Price;
        return BuildResult.Success("Structure Built");
    }


    public void AddStructure(StructureController structure) => _structures.Add(structure);
    public void RemoveStructure(StructureController structure) => _structures.Remove(structure);

}