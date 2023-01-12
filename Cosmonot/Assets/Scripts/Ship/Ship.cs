using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour{

    public CircleCollider2D circleCollider;

    //which layers to remove from perimeter
    public LayerMask removeFromPerimeter;

    //once something enters this circle collider
    void OnTriggerEnter2D(Collider2D other) {
        //check if that colliding object layermask is one that needs to be removed
        if(Helpers.HasLayer(removeFromPerimeter, other.gameObject.layer)){
            Destroy(other.gameObject);
        }
    }
}
