using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform firePoint;           // Point where bullets spawn
    public GameObject bulletPrefab;       // Bullet prefab
    public float fireRate = 1f;           // Bullets per second
    public float range = 10f;             // Tower range
    public int upgradeCost = 10;          // Cost to upgrade the tower

    private int level = 1;                // Tower level
    private float fireCooldown = 0f;      // Cooldown timer for firing

    public int Level => level;
    public int UpgradeCost => upgradeCost;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null && fireCooldown <= 0)
        {
            Shoot(closestEnemy);
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Bullet prefab or fire point is not assigned.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(target);
        }
    }

    void OnMouseDown()
    {
        TowerUpgradeUI upgradeUI = FindObjectOfType<TowerUpgradeUI>();
        if (upgradeUI != null)
        {
            Debug.Log("Tower clicked. Attempting to open upgrade menu...");
            upgradeUI.OpenUpgradeMenu(this);
        }
    }

    public void UpgradeTower()
    {
        if (GameManager.Instance.gold >= upgradeCost)
        {
            GameManager.Instance.UpdateGold(-upgradeCost);
            level++;
            fireRate += 2f;
            range += 2f;
            upgradeCost += 10;

            Debug.Log($"Tower upgraded to Level {level}. New Range: {range}, Fire Rate: {fireRate}");
        }
    }
}
