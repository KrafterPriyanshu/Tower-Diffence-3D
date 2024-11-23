using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int health = 100;
    public int damageToHouse = 10; // Damage dealt to the house
    public float baseSpeed = 3.5f; // Base speed for the enemy

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;

        // Find the house object and set it as the destination
        GameObject house = GameObject.FindGameObjectWithTag("House");
        if (house != null)
        {
            agent.SetDestination(house.transform.position);
        }
        else
        {
            Debug.LogError("No object with tag 'House' found!");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.UpdateGold(10); // Reward gold on enemy death
        Destroy(gameObject);
        Debug.Log($"{gameObject.name} has been destroyed.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House"))
        {
            Debug.Log("Enemy reached the house!");
            GameManager.Instance.TakeDamage(damageToHouse); // Damage the house
            Destroy(gameObject); // Destroy the enemy after dealing damage
        }
    }
}
