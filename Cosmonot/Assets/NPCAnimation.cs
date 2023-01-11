using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCAnimation : MonoBehaviour
{
    Animator enemyAnimator;
    AIPath a_star; 

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        a_star = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a_star.velocity.x != 0 || a_star.velocity.y != 0)
        {
            enemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }
    }
}
