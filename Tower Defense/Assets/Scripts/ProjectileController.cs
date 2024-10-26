using NaughtyAttributes;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public EnemyController Target;
    public float Speed = 5;
    public float Damage = 1;
    private float _delta = 0.1f;

    void Update()
    {
        if (Target == null) 
        { 
            GameObject.Destroy(gameObject);
            return; 
        }
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(transform.position - Target.transform.position, transform.up);

        float distance = (Target.transform.position - transform.position).magnitude;
        if (Mathf.Abs(distance) < _delta)
        {
            GameObject.Destroy(gameObject);
            Target.ApplyDamage(Damage);
        }
    }
}
