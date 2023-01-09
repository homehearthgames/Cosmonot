using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {
    // cell size squared of this item
    public int cellSize = 1;

    // is this item placed on the terrain
    public bool placed;
    // is this item in a valid place?
    public bool placeable;

    // what object layers obscure placement
    public LayerMask layerObstructions;
    // valid placement color
    public Color placeableColor;
    // blocked placement color
    public Color blockedColor;

    // reference to collider
    new Collider2D collider;
    // reference to sprite renderer
    SpriteRenderer graphics;
    // the new point of origin for obstructions check
    Vector2 obstruction_check;

    void Start() {
        // grab the sprite renderer
        graphics = GetComponentInChildren<SpriteRenderer>();
        // grab the collider
        collider = GetComponent<Collider2D>();
        // set collider as trigger so we don't push other objects around while in build mode
        collider.isTrigger = true;
        // set placed to false
        placed = false;
    }

    // this entire functions kinda sloppy will fix
    void Update() {
        if(!placed ){
            // set the point of origin for obstructions check based on the cell size: true = center | false center + 0.5f
            obstruction_check = cellSize == 1? (Vector2)transform.position : (Vector2)transform.position + Vector2.one * 0.5f;
            // check if we are colliding with anything that obscures this item
            var hits = Physics2D.OverlapBoxAll(obstruction_check, Vector2.one * (cellSize - 0.5f), layerObstructions);

            // are we hitting more obstructions than just ourself?
            placeable = hits.Length > 1 ? false : true;
            // swap the color overlay based on if its placable
            var current_color = placeable ? placeableColor : blockedColor;
            // adjust the shaders _color to our current color
            graphics.material.SetColor("_Color", current_color);
        }
    }

    // on placed 
    public void Place(){
        // adjust the shaders _color to transparent / remove the overlay color
        graphics.material.SetColor("_Color", new Color(1,1,1,0));
        // set this collider back for proper collisions
        collider.isTrigger = false;
        placed = true;
        // disable this script after this item is placed
        enabled = false;
    }

    // debug info for the obstructions point of origin + size
    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(obstruction_check, Vector2.one * (cellSize - 0.5f));
    }
}
