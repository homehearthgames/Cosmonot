using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour{

    //ships detection radius; 
    public float radius = 10;
    //which layers to remove from perimeter
    public LayerMask removeFromPerimeter;

    public List<GameObject> objectsInRange;

    public float updateMessageRate = 1;

    //collider for collisions
    public new CircleCollider2D collider {get; private set;}

    void Start() {
        //grab this circle collider
        collider = GetComponent<CircleCollider2D>();
        //adjust the circles collider to the detection radius;
        collider.radius = radius;
        InvokeRepeating("UpdateRangedObjects", updateMessageRate, updateMessageRate);
    }

    void UpdateRangedObjects(){
        foreach (var obj in objectsInRange) {
            // obj.SendMessage("ShipUpdate", SendMessageOptions.DontRequireReceiver);
        }
    }

    //once something enters this circle collider
    void OnTriggerEnter2D(Collider2D other) {
        objectsInRange.Add(other.gameObject);
        other.SendMessage("EnterShipRange", SendMessageOptions.DontRequireReceiver);

        //check if that colliding object layermask is one that needs to be removed
        if(HasLayer(removeFromPerimeter, other.gameObject.layer)){
            //destroy that colliding object
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        objectsInRange.Remove(other.gameObject);
        other.SendMessage("ExitShipRange", SendMessageOptions.DontRequireReceiver);
    }

    //check if any value has changed on this script
    void OnValidate() {
        //grab this circle collider
        collider = GetComponent<CircleCollider2D>();
        //adjust the circles collider to the detection radius;
        collider.radius = radius;
    }

    // check if a layermask contain a specific layer.. probably should move this to a helper class.
    bool HasLayer(LayerMask layerMask, int layer) {
        // is the layer we're looking for the current layer? or bit shift till we find a match
        if (layerMask == (layerMask | (1 << layer))) {
            //return true if so
            return true;
        }
        //return false if not
        return false;
    }
}
