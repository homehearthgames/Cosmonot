using System.Collections;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    // Reference to the EnemyTracker script
    private EnemyTracker enemyTracker;

    // Reference to the Health script
    private Health health;

    // The amount of damage the turret will deal
    public int damage = 10;

    // The interval in seconds at which the turret will deal damage
    public float interval = 1.0f;

    private void Start()
    {
        // Get the EnemyTracker component on the same game object
        enemyTracker = GetComponent<EnemyTracker>();

        // Start the attack coroutine
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            // Wait for the interval
            yield return new WaitForSeconds(interval);

            // Get the nearest enemy
            GameObject nearestEnemy = enemyTracker.FindNearestEnemy();

            // If there is a nearest enemy
            if (nearestEnemy != null)
            {
                // Get the Health component on the nearest enemy
                health = nearestEnemy.GetComponent<Health>();

                // Deal damage to the nearest enemy
                health.TakeDamage(damage);
            }
        }
    }
}
