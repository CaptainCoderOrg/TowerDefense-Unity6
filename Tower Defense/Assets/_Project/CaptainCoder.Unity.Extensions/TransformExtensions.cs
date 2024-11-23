using System;
using UnityEngine;
namespace CaptainCoder.Unity.Extensions
{
    public static class TransformExtensions
    {

        public static void DestroyChildren(this Transform transform)
        {
            Action<GameObject> destroy = GameObject.Destroy;
            if (!Application.isPlaying)
            {
                destroy = GameObject.DestroyImmediate;
            }
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                destroy.Invoke(transform.GetChild(i).gameObject);
            }
        }

    }
}