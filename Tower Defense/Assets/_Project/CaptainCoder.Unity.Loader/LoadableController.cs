using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CaptainCoder.Unity.Loader
{
    public sealed class LoadableController : MonoBehaviour
    {
        public UnityEvent[] OnLoad;

        public IEnumerator Load()
        {
            foreach (UnityEvent e in OnLoad)
            {
                e.Invoke();
                yield return null;
            }
        }
        
    }
}