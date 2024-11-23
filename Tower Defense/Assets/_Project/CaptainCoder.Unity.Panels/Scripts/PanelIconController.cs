using UnityEngine;
using UnityEngine.UI;

namespace CaptainCoder.Unity.Panels
{
    public sealed class PanelIconController : MonoBehaviour
    {
        private Button _button;
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
            _button = GetComponentInChildren<Button>();
            _animator = GetComponent<Animator>();
            Debug.Assert(Panel != null, $"{nameof(PanelController)} was not set before Awake");
        }

        void OnEnable()
        {
            _button.onClick.AddListener(Toggle);
            Panel.OnShow.AddListener(Show);
            Panel.OnHide.AddListener(Hide);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(Toggle);
            Panel.OnShow.RemoveListener(Show);
            Panel.OnHide.RemoveListener(Hide);
        }

        private void Toggle() => Panel.Toggle();

        private void Show() => _animator.SetTrigger("Show");
        private void Hide() => _animator.SetTrigger("Hide");
    }
}