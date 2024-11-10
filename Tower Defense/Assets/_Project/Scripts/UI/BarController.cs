using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour 
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Image _image;
    public float Value 
    {
        get => _image.rectTransform.localScale.x;
        set 
        {
            Vector3 scale = _image.rectTransform.localScale;
            scale.x = value;
            _image.rectTransform.localScale = scale; 
            _animator.SetTrigger("Show");
        }
    }

    public void RenderEnemyHealth(EnemyController enemyController)
    {
        Value = enemyController.Health / enemyController.BaseHealth;
    }

}