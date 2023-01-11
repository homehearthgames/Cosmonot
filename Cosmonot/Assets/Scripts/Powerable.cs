using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Powerable : MonoBehaviour{
    public bool powered;

    public UnityEvent onPowered;
    public UnityEvent onLostPowered;

    public void OnPowercellEnter(){
        powered = true;
        onPowered.Invoke();
    }
    public void OnPowercellUpdate(){
        powered = true;
    }
    public void OnPowercellExit(){
        powered = false;
        onLostPowered.Invoke();
    }
}
