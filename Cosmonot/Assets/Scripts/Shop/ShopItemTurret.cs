using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTurret: ShopItem{
    public bool powercellRange;

    public override void Update() {
        if(!placed ){
            // set the point of origin for obstructions check based on the cell size: true = center | false center + 0.5f
            obstruction_check = cellSize == 1? (Vector2)transform.position : (Vector2)transform.position + Vector2.one * 0.5f;
            // check if we are colliding with anything that obscures this item
            var hits = Physics2D.OverlapBoxAll(obstruction_check, Vector2.one * (cellSize - 0.5f), layerObstructions);

            // are we hitting more obstructions than just ourself?
            placeable = !ContainsObstruction(hits) && powercellRange ? true : false;
            // swap the color overlay based on if its placable
            var current_color = placeable ? placeableColor : blockedColor;
            // adjust the shaders _color to our current color
            SetSpriteColor(current_color);
        }
    }

    public void OnPowercellUpdate(){
        powercellRange = true;
    }

    public void OnPowercellExit(){
        powercellRange = false;
    }
}
