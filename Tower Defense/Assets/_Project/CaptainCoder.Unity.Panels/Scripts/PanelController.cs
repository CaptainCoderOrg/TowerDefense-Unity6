using UnityEngine;
using UnityEngine.Events;

namespace CaptainCoder.Unity.Panels
{
    public sealed class PanelController : MonoBehaviour
    {
        private Animator _animator;
        public bool IsShowing { get; private set; }
        public UnityEvent OnShow;
        public UnityEvent OnHide;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Toggle()
        {
            if (IsShowing)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void Show()
        {
            if (!IsShowing)
            {
                IsShowing = true;
                _animator.SetTrigger("Show");
                OnShow.Invoke();
            }
        }

        public void Hide()
        {
            if (IsShowing)
            {
                IsShowing = false;
                _animator.SetTrigger("Hide");
                OnHide.Invoke();
            }
        }
    }
}