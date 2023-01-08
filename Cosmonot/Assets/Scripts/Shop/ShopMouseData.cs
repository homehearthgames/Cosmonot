using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopMouseData : MonoBehaviour {

    public Vector2 coordinates;

    void Update() {
        coordinates = RoundVector(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    Vector2 RoundVector(Vector2 value){
        return new Vector2Int(
            Mathf.RoundToInt(value.x),
            Mathf.RoundToInt(value.y)
        );
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(RoundVector(coordinates), Vector2.one);
    }
}
