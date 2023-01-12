using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalEmitter : MonoBehaviour{

    public BoxCollider2D radiusCollider;
    public float messageRate = 1;
    public LayerMask layersToCheck;
    public string repeatingMessageMethodName;
    public string enterMessageMethodName;
    public string exitMessageMethodName;

    public List<GameObject> object_in_proximity;// public for debugging

    void Start() {
        object_in_proximity = new();
        InvokeRepeating("Tick", 0, messageRate);
        radiusCollider.isTrigger = true;
    }

    public void Restart(){
        var in_range = Physics2D.OverlapBoxAll((Vector2)transform.position + radiusCollider.offset, radiusCollider.bounds.size, 0);
        foreach (var item in in_range) {
            object_in_proximity.Add(item.gameObject);
        }
    }

    void Tick(){
        for (int i = 0; i < object_in_proximity.Count; i++) {
            if(object_in_proximity[i] == null){
                object_in_proximity.RemoveAt(i);
                continue;
            }
            
            if(!Helpers.HasLayer(layersToCheck, object_in_proximity[i].gameObject.layer)) continue;

            if(repeatingMessageMethodName == ""){
                Debug.LogWarning($"{gameObject.name}: {this}, message method name is empty or null!");
                return;
            }
            
            object_in_proximity[i].SendMessage(repeatingMessageMethodName, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        object_in_proximity.Add(other.gameObject);
        if(enterMessageMethodName == ""){
            Debug.LogWarning($"{gameObject.name}: {this}, enter message method name is empty or null!");
            return;
        }
        other.gameObject.SendMessage(enterMessageMethodName, SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerExit2D(Collider2D other) {
        object_in_proximity.Remove(other.gameObject);
        if(exitMessageMethodName == ""){
            Debug.LogWarning($"{gameObject.name}: {this}, exit message method name is empty or null!");
            return;
        }
        other.gameObject.SendMessage(exitMessageMethodName, SendMessageOptions.DontRequireReceiver);
    }

    void OnValidate() {
        if(radiusCollider == null) return;
        radiusCollider.isTrigger = true;
    }
}
