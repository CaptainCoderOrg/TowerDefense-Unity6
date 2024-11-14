using TMPro;
using UnityEngine;

public class VictoryScreenController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeLabel;
    [SerializeField]
    private TextMeshProUGUI _damageLabel;
    [SerializeField]
    private TextMeshProUGUI _enemiesLabel;
    [SerializeField]
    private TextMeshProUGUI _energyLabel;
    private Animator _animator;
    private bool _isShowing = false;


    void Awake()
    {
        Debug.Assert(_timeLabel != null, "Time label is not set in the inspector");
        Debug.Assert(_damageLabel != null, "Damage label is not set in the inspector");
        Debug.Assert(_enemiesLabel != null, "Enemies label is not set in the inspector");
        Debug.Assert(_energyLabel != null, "Energy label is not set in the inspector");
        _isShowing = false;
        _animator = GetComponent<Animator>();
        GameManagerController.Instance.OnGameWon.AddListener(ShowVictoryScreen);
    }

    private void ShowVictoryScreen() => ShowVictoryScreen(GameManagerController.Instance.Stats);

    private void ShowVictoryScreen(GameStats gameStats)
    {
        _timeLabel.text = $"Time: {gameStats.Time}";
        _damageLabel.text = $"Damage Sustained: {gameStats.DamageSustained:#.##}";
        _enemiesLabel.text = $"Enemies Defeated: {gameStats.EnemiesDefeated} / {gameStats.EnemiesSpawned}";
        _energyLabel.text = $"Energy Collected: {gameStats.EnergyCollected}";
        if (!_isShowing)
        {
            _isShowing = true;
            _animator.SetTrigger("Show");
        }
    }
}
