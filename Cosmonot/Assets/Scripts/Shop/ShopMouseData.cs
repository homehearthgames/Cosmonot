using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopMouseData : MonoBehaviour {

    //coordinates of the mouse pointer
    public Vector2 coordinates;

    void Update() {
        //coordinates of the mouse pointer
        coordinates = RoundVector(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    //round the vector 2 to the nearest int
    Vector2 RoundVector(Vector2 value){
        return new Vector2Int(
            Mathf.RoundToInt(value.x),
            Mathf.RoundToInt(value.y)
        );
    }

    //draw a gizmos for the editor view
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(RoundVector(coordinates), Vector2.one);
    }
}
