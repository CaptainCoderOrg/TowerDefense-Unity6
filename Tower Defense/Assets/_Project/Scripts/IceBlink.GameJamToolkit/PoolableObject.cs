using UnityEngine;

// Used with permission of the Author
// https://github.com/AlCh3mi/GameJamToolKit/blob/main/GameJamToolkit/ObjectPooling/PoolableObject.cs
namespace IceBlink.GameJamToolkit.ObjectPooling
{
    public class PoolableObject : MonoBehaviour
    {
        public delegate void ReturnToPoolEvent(PoolableObject poolableObject);
    
        public event ReturnToPoolEvent ReturnToPool;

        protected virtual void OnDisable()
        {
            ReturnToPool?.Invoke(this);
        }
    }
}