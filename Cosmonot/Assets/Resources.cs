using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    // The amount of carbon of carbon or scrap this enemy awards on death
    [SerializeField] int carbon;
    [SerializeField] int scrap;

    public int Carbon
    {
        get { return carbon; }
    }
    
    public int Scrap
    {
        get { return scrap; }
    }
}
