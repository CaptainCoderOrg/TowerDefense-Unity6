using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CaptainCoder.Unity.Loader
{
    public sealed class LoaderController : MonoBehaviour
    {
        [field: SerializeField]
        public UnityEvent OnFinished { get; private set; }
        [field: SerializeField]
        public LoaderManifest Manifest { get; private set; }
        [field: SerializeField]
        public List<LoadableController> InSceneAssets { get; private set; }

        IEnumerator Start()
        {
            foreach (LoadableController loadable in Manifest.Loaders)
            {
                long start = Time();
                var loaded = GameObject.Instantiate(loadable, transform);
                IEnumerator enumerator = loaded.Load();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                long end = Time();
                Debug.Log($"Loaded {loaded} in {end - start}ms");
                yield return null;
            }

            foreach (LoadableController loadable in InSceneAssets)
            {
                long start = Time();
                IEnumerator enumerator = loadable.Load();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                long end = Time();
                Debug.Log($"Loaded {loadable} in {end - start}ms");
                yield return null;
            }

            foreach (var simple in Manifest.SimpleAssets)
            {
                long start = Time();
                var loaded = GameObject.Instantiate(simple, transform);
                long end = Time();
                Debug.Log($"Loaded {loaded} in {end - start}ms");
                yield return null;
            }

            OnFinished.Invoke();
            yield return null;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private long Time() => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}