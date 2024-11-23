using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;    // Speed of the bullet
    public int damage = 20;     // Damage dealt to the target
    private Transform target;   // The target of the bullet

    // Initialize the bullet with the target
    public void Initialize(Transform enemyTarget)
    {
        target = enemyTarget;
        Debug.Log($"Bullet initialized with target: {target?.name}");
    }

    void Update()
    {
        if (target == null)
        {
            Debug.Log("Bullet target is null. Destroying bullet.");
            Destroy(gameObject); // Destroy bullet if target no longer exists
            return;
        }

        // Move the bullet toward the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if the bullet has reached the target
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        // Apply damage to the target if it has the EnemyAI component
        if (target.TryGetComponent(out EnemyAI enemy))
        {
            enemy.TakeDamage(damage);
            Debug.Log($"Bullet hit {target.name}, dealing {damage} damage.");
        }

        Destroy(gameObject); // Destroy the bullet after hitting
    }
}
