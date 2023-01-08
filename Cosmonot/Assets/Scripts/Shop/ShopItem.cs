using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {
    public int cellSize = 1;

    public bool placed;
    public bool placeable;

    public LayerMask layerObstructions;
    public Color placeableColor;
    public Color blockedColor;


    new Collider2D collider;
    SpriteRenderer graphics;
    Vector2 obstruction_check;

    void Start() {
        graphics = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
        placed = false;
    }

    void Update() {
        if(!placed ){
            obstruction_check = cellSize == 1? (Vector2)transform.position : (Vector2)transform.position + Vector2.one * 0.5f;
            var hits = Physics2D.OverlapBoxAll(obstruction_check, Vector2.one * (cellSize - 0.5f), layerObstructions);

            placeable = hits.Length > 1 ? false : true;
            var current_color = placeable ? placeableColor : blockedColor;
            graphics.material.SetColor("_Color", current_color);
        }
    }

    public void Place(){
        enabled = false;
        graphics.material.SetColor("_Color", new Color(1,1,1,0));
        collider.isTrigger = false;
        placed = true;
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(obstruction_check, Vector2.one * (cellSize - 0.5f));
    }
}
