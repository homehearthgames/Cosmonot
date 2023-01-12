﻿using UnityEngine;
using System.Collections;
using Pathfinding;
public class WanderingDestinationSetter : MonoBehaviour {
    public float radius = 20;
    IAstarAI ai;
    void Start () {
        ai = GetComponent<IAstarAI>();
    }
    Vector2 PickRandomPoint () {
        var point = Random.insideUnitSphere * radius;
        point += ai.position;
        return point;
    }
    void Update () {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedDestination || !ai.hasPath)) {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }

        // redundance for running into a collider and cant reach destination
        if(ai.velocity.magnitude > 1 && ai.hasPath && !ai.reachedDestination ){
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }
}