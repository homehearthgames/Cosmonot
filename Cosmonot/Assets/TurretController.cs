using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    TurretAttack turretAttack;
    EnemyTracker enemyTracker;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetComponents(){
        turretAttack = GetComponent<TurretAttack>();
        enemyTracker = GetComponent<EnemyTracker>();
        animator = GetComponent<Animator>();
    }

    public void DisableTurret()
    {
        GetComponents();
        turretAttack.enabled = false;
        enemyTracker.enabled = false;
        animator.SetBool("isFiring", false);
    }

    public void EnableTurret()
    {
        GetComponents();
        enemyTracker.enabled = true;
        turretAttack.enabled = true;
    }
}
