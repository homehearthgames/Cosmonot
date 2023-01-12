using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    // The layer on which the enemies are
    public LayerMask enemyLayer;

    // The transform of the weapon
    private Transform weaponTransform;

    // The maximum distance at which the weapon will aim at enemies
    public float maxAimDistance = 10.0f;

    // The angle through which the weapon will scan for enemies when no enemies are within range
    public float scanningAngle = 180.0f;

    // The speed at which the weapon will scan for enemies
    public float scanningSpeed = 90.0f;

    // The animator component
    private Animator weaponAnimator;

    void Start()
    {
        // Get the transform of the weapon
        weaponTransform = transform;
        
        // Get the animator component
        weaponAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Find the nearest enemy within the maximum aim distance
        GameObject nearestEnemy = FindNearestEnemy();

        // If an enemy was found, aim the weapon at it
        if (nearestEnemy != null)
        {
            Vector3 direction = nearestEnemy.transform.position - weaponTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Set the isFiring parameter in the animator to true
            weaponAnimator.SetBool("isFiring", true);
        }
        // If no enemy was found, scan for enemies
        else
        {
            float currentAngle = weaponTransform.rotation.eulerAngles.z;
            float targetAngle = currentAngle + scanningSpeed * Time.deltaTime;
            if (targetAngle > 360.0f)
            {
                targetAngle -= 360.0f;
            }
            weaponTransform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

            // Set the isFiring parameter in the animator to false
            weaponAnimator.SetBool("isFiring", false);
        }
    }

    public GameObject FindNearestEnemy()
    {
        // Get a list of all the colliders in the scene within the maximum aim distance
        Collider2D[] colliders = Physics2D.OverlapCircleAll(weaponTransform.position, maxAimDistance, enemyLayer);

        // Keep track of the nearest enemy
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // Iterate through the colliders and find the nearest enemy
        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(weaponTransform.position, collider.transform.position);
            if (distance < nearestDistance)
            {
                nearestEnemy = collider.gameObject;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }
}
