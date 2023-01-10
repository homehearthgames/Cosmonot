using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopMouseData : MonoBehaviour {

    //coordinates of the mouse pointer
    public Vector2 coordinates;

    void Update() {
        //coordinates of the mouse pointer
        coordinates = Helpers.RoundVector(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    //draw a gizmos for the editor view
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Helpers.RoundVector(coordinates), Vector2.one);
    }
}
