using System.Collections;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    
    [SerializeField] ParticleSystem particleSystem1, particleSystem2;
    // Reference to the EnemyTracker script
    private EnemyTracker enemyTracker;

    // Reference to the Health script
    private Health health;

    // The amount of damage the turret will deal
    public int damage = 10;

    // The interval in seconds at which the turret will deal damage
    public float interval = 1.0f;
    float timer;

    private void Start() 
    {
        // Get the EnemyTracker component on the same game object
        enemyTracker = GetComponent<EnemyTracker>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= interval)
        {

            GameObject nearestEnemy = enemyTracker.FindNearestEnemy();
                
            // If there is a nearest enemy
            if (nearestEnemy != null)
            {
                if (particleSystem1 != null)
                {
                    particleSystem1.Play();
                }
                if (particleSystem2 != null)
                {
                    particleSystem2.Play();
                }
                // Get the Health component on the nearest enemy
                health = nearestEnemy.GetComponent<Health>();

                // Deal damage to the nearest enemy
                health.TakeDamage(damage);
            } else
            {
                if (particleSystem1 != null)
                {
                    particleSystem1.Stop();
                }
                if (particleSystem2 != null)
                {
                    particleSystem2.Stop();
                }
            }
            timer = 0;
        }
    }
}
