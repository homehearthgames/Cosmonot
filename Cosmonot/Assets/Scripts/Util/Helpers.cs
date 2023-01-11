using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers{
    //round the vector 2 to the nearest int
    public static Vector2 RoundVector(Vector2 value){
        return new Vector2Int(
            Mathf.RoundToInt(value.x),
            Mathf.RoundToInt(value.y)
        );
    }

    // check if a layermask contain a specific layer
    public static bool HasLayer(LayerMask layerMask, int layer) {
        // is the layer we're looking for the current layer? or bit shift till we find a match
        if (layerMask == (layerMask | (1 << layer))) {
            //return true if so
            return true;
        }
        //return false if not
        return false;
    }

    // // check if a layermask contain a specific layer
    // public static bool HasLayer(LayerMask layerMask, int layer) {
    //     // is the layer we're looking for the current layer? or bit shift till we find a match
    //     if (layerMask == (layerMask | (1 << layer))) {
    //         //return true if so
    //         return true;
    //     }
    //     //return false if not
    //     return false;
    // }
}
