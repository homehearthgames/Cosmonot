using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    TurretAttack turretAttack;
    EnemyTracker enemyTracker;
    // Start is called before the first frame update
    void Start()
    {
        turretAttack = GetComponent<TurretAttack>();
        enemyTracker = GetComponent<EnemyTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableTurret()
    {
        turretAttack.enabled = false;
        enemyTracker.enabled = false;
    }

    public void EnableTurret()
    {
        enemyTracker.enabled = true;
        turretAttack.enabled = true;
    }
}
