using UnityEngine;

public class VictoryScreenController : MonoBehaviour
{

    private Animator _animator;
    private bool _isShowing = false;

    void Awake()
    {
        _isShowing = false;
        _animator = GetComponent<Animator>();
        GameManagerController.Instance.OnGameWon.AddListener(ShowVictoryScreen);
    }

    public void ShowVictoryScreen()
    {
        if (!_isShowing)
        {
            _isShowing = true;
            _animator.SetTrigger("Show");
        }
    }
}
