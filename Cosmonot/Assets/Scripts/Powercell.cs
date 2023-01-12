using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Powercell : MonoBehaviour{

    public bool powered;
    public UnityEvent onPowered;
    public UnityEvent onLostPower;
    public void OnGeneratorUpdate(){
        if(!powered){
            onPowered.Invoke();
            powered = true;
        }
    }
    public void OnGeneratorExit(){
        powered = false;
        onLostPower.Invoke();
    }
}
