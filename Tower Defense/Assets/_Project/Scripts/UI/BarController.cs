using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour 
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Image _image;
    public float Duration = 1;
    private float _startTime = 0;
    private float _targetValue = 1;
    private float _currentValue = 1;
    private float _startValue = 1;
    public float Value 
    {
        get => _targetValue;
        set 
        {
            _startTime = Time.time;
            _startValue = _currentValue;
            _targetValue = value;
            _animator.SetTrigger("Show");
        }
    }

    public void Update()
    {
        float percent = Mathf.Clamp01((Time.time - _startTime) / Duration);
        _currentValue = Mathf.Lerp(_startValue, _targetValue, percent);
        Vector3 scale = _image.rectTransform.localScale;
        scale.x = _currentValue;
        _image.rectTransform.localScale = scale; 
    }

    

    public void RenderEnemyHealth(EnemyController enemyController)
    {
        Value = enemyController.Health / enemyController.BaseHealth;
    }

}