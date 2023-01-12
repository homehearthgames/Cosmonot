using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Powered : MonoBehaviour{
    public bool powered;
    public UnityEvent onPowered;
    public UnityEvent onLostPower;
    public void OnPowercellUpdate(){
        if(!powered){
            onPowered.Invoke();
            powered = true;
        }
    }
    public void OnPowercellExit(){
        powered = false;
        onLostPower.Invoke();
    }
}
