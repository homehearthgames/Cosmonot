using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Shop : MonoBehaviour{
    public List<GameObject> items;
    public UnityEvent OnPlaced;

    ShopItem item_to_build;
    ShopMouseData mouse_data;
    bool building;

    void Start() {
        mouse_data = GetComponent<ShopMouseData>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            PurchaseItem(Random.Range(0, items.Count));
        }

        if(!building) return;

        item_to_build.transform.position = mouse_data.coordinates;
        if(Mouse.current.leftButton.wasReleasedThisFrame && item_to_build.placeable){
            PlaceItem();
        }
    }

    public void PurchaseItem(int index){
        if(building) return;

        building = true;
        item_to_build = Instantiate(items[index], mouse_data.coordinates, Quaternion.identity).GetComponent<ShopItem>();
    }

    void PlaceItem(){
        OnPlaced.Invoke();
        item_to_build.Place();
        item_to_build = null;
        building = false;
    }
}