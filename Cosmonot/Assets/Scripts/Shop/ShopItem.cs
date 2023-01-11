using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent onPlaced;

    // reference to collider
    new Collider2D collider;
    // reference to sprite renderer
    SpriteRenderer[] graphics;
    // the new point of origin for obstructions check
    Vector2 obstruction_check;

    bool ship_range;

    void Start() {
        // grab the sprite renderer
        graphics = GetComponentsInChildren<SpriteRenderer>();
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
            placeable = !ContainsObstruction(hits) && ship_range ? true : false;
            // swap the color overlay based on if its placable
            var current_color = placeable ? placeableColor : blockedColor;
            // adjust the shaders _color to our current color
            SetSpriteColor(current_color);
        }
    }

    bool ContainsObstruction(Collider2D[] hits){
        foreach (var item in hits) {
            if(item.gameObject == this.gameObject) continue;
            // is the layer we're looking for the current layer? or bit shift till we find a match
            if (layerObstructions == (layerObstructions | (1 << item.gameObject.layer))) {
                //return true if so
                return true;
            }
        }
        return false;
    }


    // on placed 
    public void Place(){
        // adjust the shaders _color to transparent / remove the overlay color
        SetSpriteColor (new Color(1,1,1,0));
        // set this collider back for proper collisions
        collider.isTrigger = false;
        placed = true;
        FindObjectOfType<PathfindingUpdateObstacles>().RecalculatePathfinding(collider);
        onPlaced.Invoke();
        // disable this script after this item is placed
        enabled = false;
    }

    [ContextMenu("Place me!")]
    void PlacedFromEditor(){
        // grab the sprite renderer
        graphics = GetComponentsInChildren<SpriteRenderer>();
        // grab the collider
        collider = GetComponent<Collider2D>();
        transform.position = Helpers.RoundVector(transform.position);
        Place();
    }

    void SetSpriteColor(Color color){
        foreach (var sprite in graphics) {
            sprite.material.SetColor("_Color", color);
        }
    }

    public void OnShipUpdate(){
        ship_range = true;
    }

    public void OnShipExit(){
        ship_range = false;
    }

    // debug info for the obstructions point of origin + size
    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(obstruction_check, Vector2.one * (cellSize - 0.5f));
    }
}
