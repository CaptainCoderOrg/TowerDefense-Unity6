using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenController : MonoBehaviour
{
    private GameManagerController _gameManager;
    [SerializeField]
    private TextMeshProUGUI _timeLabel;
    [SerializeField]
    private TextMeshProUGUI _damageLabel;
    [SerializeField]
    private TextMeshProUGUI _enemiesLabel;
    [SerializeField]
    private TextMeshProUGUI _energyLabel;
    [SerializeField]
    private TextMeshProUGUI _structuresLabel;
    private Animator _animator;
    private bool _isShowing = false;


    void Awake()
    {
        _gameManager = GetComponentInParent<GameManagerController>();
        Debug.Assert(_gameManager != null, $"Cannot find {nameof(GameManagerController)}");
        Debug.Assert(_timeLabel != null, "Time label is not set in the inspector");
        Debug.Assert(_damageLabel != null, "Damage label is not set in the inspector");
        Debug.Assert(_enemiesLabel != null, "Enemies label is not set in the inspector");
        Debug.Assert(_energyLabel != null, "Energy label is not set in the inspector");
        Debug.Assert(_structuresLabel != null, "Structures label is not set in the inspector");
        _isShowing = false;
        _animator = GetComponent<Animator>();
        _gameManager.OnGameWon.AddListener(ShowVictoryScreen);
        Button continueButton = GetComponentInChildren<Button>();
        continueButton.onClick.AddListener(HandleContinue);
    }

    public void HandleContinue()
    {
        SceneManager.LoadScene("Title Screen");
    }

    private void ShowVictoryScreen() => ShowVictoryScreen(_gameManager.Stats);

    private void ShowVictoryScreen(GameStats gameStats)
    {
        _timeLabel.text = $"Time: {gameStats.Time}";
        _damageLabel.text = $"Damage Sustained: {gameStats.DamageSustained:#.##}";
        _enemiesLabel.text = $"Enemies Defeated: {gameStats.EnemiesDefeated} / {gameStats.EnemiesSpawned}";
        _energyLabel.text = $"Energy Collected: {gameStats.EnergyCollected}";
        _structuresLabel.text = $"Structures Built: {gameStats.StructuresBuilt}";
        if (!_isShowing)
        {
            _isShowing = true;
            _animator.SetTrigger("Show");
        }
    }
}
