using TMPro;
using UnityEngine;

public class VictoryScreenController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeLabel;
    [SerializeField]
    private TextMeshProUGUI _damageLabel;
    private Animator _animator;
    private bool _isShowing = false;
    

    void Awake()
    {
        Debug.Assert(_timeLabel != null, "Time label is not set in the inspector");
        Debug.Assert(_damageLabel != null, "Damage label is not set in the inspector"); 
        _isShowing = false;
        _animator = GetComponent<Animator>();
        GameManagerController.Instance.OnGameWon.AddListener(ShowVictoryScreen);
    }

    private void ShowVictoryScreen() => ShowVictoryScreen(GameManagerController.Instance.Stats);

    private void ShowVictoryScreen(GameStats gameStats)
    {
        _timeLabel.text = $"Time: {gameStats.Time}";
        _damageLabel.text = $"Damage Sustained: {gameStats.DamageSustained:#.##}";
        if (!_isShowing)
        {
            _isShowing = true;
            _animator.SetTrigger("Show");
        }
    }
}
