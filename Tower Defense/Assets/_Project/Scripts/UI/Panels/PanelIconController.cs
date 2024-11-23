using UnityEngine;
using UnityEngine.UI;

public sealed class PanelIconController : MonoBehaviour 
{
    private Animator _animator;
    [SerializeField]
    private Image _image;
    public PanelController Panel { get; set; }
    [SerializeField]
    private Image _imageShadow;
    private Sprite _sprite;
    public Sprite Icon 
    {
        get => _sprite;
        set
        {
            _sprite = value;
            _image.sprite = _sprite;
            _imageShadow.sprite = _sprite;
        }
    }

    void Awake()
    {
        Button button = GetComponentInChildren<Button>();
        button.onClick.AddListener(Toggle);
        _animator = GetComponent<Animator>();
    }
    
    public void Toggle()
    {
        if (Panel.IsShowing)
        {
            Panel.Hide();
            _animator.SetTrigger("Hide");
        }
        else
        {
            Panel.Show();
            _animator.SetTrigger("Show");
        }
    }
}