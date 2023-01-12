using UnityEngine;
using System.Collections;
using Pathfinding;
public class WanderingDestinationSetter : MonoBehaviour {
    public float radius = 20;
    IAstarAI ai;
    public float minWanderInterval = 5f;
    public float maxWanderInterval = 10f;
    public bool canWander;

void Start() {
    ai = GetComponent<IAstarAI>();
    canWander = true;
    float wanderInterval = Random.Range(minWanderInterval, maxWanderInterval);
    InvokeRepeating("Wander", 0f, wanderInterval);
}

void Wander(){
    if (!ai.pathPending && (ai.reachedDestination || !ai.hasPath)) {
        ai.destination = PickRandomPoint();
        ai.SearchPath();
    }

    if(ai.velocity.magnitude > 1 && ai.hasPath && !ai.reachedDestination){
        ai.destination = PickRandomPoint();
        ai.SearchPath();
    }
}




    Vector2 PickRandomPoint () {
        var point = Random.insideUnitSphere * radius;
        point += ai.position;
        return point;
    }
}