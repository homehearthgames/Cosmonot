using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour{

    //which layers to remove from perimeter
    public LayerMask removeFromPerimeter;
    SignalEmitter signal_emitter;

    void Start() {
        signal_emitter = GetComponent<SignalEmitter>();
    }

    //once something enters this circle collider
    void OnTriggerEnter2D(Collider2D other) {
        //check if that colliding object layermask is one that needs to be removed
        if(Helpers.HasLayer(removeFromPerimeter, other.gameObject.layer)){
            //destroy that colliding object
            signal_emitter.object_in_proximity.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
