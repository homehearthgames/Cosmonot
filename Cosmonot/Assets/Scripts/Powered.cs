using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Powered : MonoBehaviour{
    public bool powered;
    public bool placed;
    public UnityEvent onPowered;
    public UnityEvent onLostPower;

    void Start() {
        OnPowercellExit();
    }
    public void OnPowercellUpdate(){
        if(!powered && placed){
            onPowered.Invoke();
            powered = true;
        }
    }
    public void OnPowercellExit(){
        powered = false;
        onLostPower.Invoke();
    }

    public void Placed(bool value){
        placed = value;
    }
}
