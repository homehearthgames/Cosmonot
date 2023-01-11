using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PathfindingUpdateObstacles : MonoBehaviour{

    public void RecalculatePathfinding(Collider2D collider){
        // As an example, use the bounding box from the attached collider
        Bounds collider_bounds = collider.bounds;
        // create new graph data
        var graph_data = new GraphUpdateObject(collider_bounds);

        // update the graph physics
        graph_data.updatePhysics = true;
        // send that data to the a stair paths for updating 
        AstarPath.active.UpdateGraphs(graph_data);
    }
}
