using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Shop : MonoBehaviour{
    //list of items in the shop for purchase
    public List<GameObject> items;
    // once a item is purchased and placed down event
    public UnityEvent OnPlaced;

    //reference to the shop item script for data
    ShopItem item_to_build;
    //grab the mouse data script
    ShopMouseData mouse_data;
    // are we currently building?
    bool building;

    void Start() {
        //grab the mouse data script
        mouse_data = GetComponent<ShopMouseData>();
    }

    void Update(){
        // for debug purposes only
        if(Input.GetKeyDown(KeyCode.P)){
            // purchase a random item from our list
            PurchaseItem(Random.Range(0, items.Count));
        }

        // if we haven't purchased a item return out of the method
        if(!building) return;

        // make the purchased item follow the mouse
        item_to_build.transform.position = mouse_data.coordinates;
        //if the clicked and is in a valid position place the item
        if(Mouse.current.leftButton.wasReleasedThisFrame && item_to_build.placeable){
            PlaceItem();
        }
    }

    // purchase a item from the shop, passing a index to which item we want. should add costs here once resources are in.
    public void PurchaseItem(int index){ // index (0-1), costs: carbon (0-nan), scrap (0-nan)
        // if we've already purchased a item and are waiting to build it return out of the method
        if(building) return;

        building = true;
        //create a new game object of our purchased item and grab the shop item data from it
        item_to_build = Instantiate(items[index], mouse_data.coordinates, Quaternion.identity).GetComponent<ShopItem>();
        // remove (clone) from the newly spawn item
        item_to_build.name = $"{items[index].name}";
    }

    // once'd placed
    void PlaceItem(){
        //call any events added to the shop
        OnPlaced.Invoke();
        //purchased item is now placed
        item_to_build.Place();
        // clear our current item, because we placed it down
        item_to_build = null;
        // because we placed it we're no longer building
        building = false;
    }
}