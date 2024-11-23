using System.Collections.Generic;
using UnityEngine;
public static class MonoBehaviourExtensions
{

    public static IEnumerable<T> GetComponents<T>(this IEnumerable<MonoBehaviour> collection)
    {
        foreach (MonoBehaviour behaviour in collection)
        {
            if (behaviour.TryGetComponent<T>(out var value))
            {
                yield return value;
            }
        }
    }

}