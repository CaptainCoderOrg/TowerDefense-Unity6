using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.Unity.Loader
{
    [CreateAssetMenu]
    public sealed class LoaderManifest : ScriptableObject
    {
        [field: SerializeField]
        public List<LoadableController> Loaders { get; private set; }
        [field: SerializeField]
        public List<GameObject> SimpleAssets { get; private set; }
    }
}