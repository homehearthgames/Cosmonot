using UnityEngine;

public class PlayerResources : MonoBehaviour 
{    
    // Singleton instance of the PlayerResources script
    public static PlayerResources instance;


    [SerializeField] int carbon;
    [SerializeField] int scrap;

    void Awake()
    {
        // Set the instance to this script
        instance = this;
    }
    
    public int Carbon {
        get { return carbon; }
        set { carbon = value; }
    }

    public int Scrap {
        get { return scrap; }
        set { scrap = value; }
    }

    public void AddCarbon(int amount) {
    carbon += amount;
    }

    public void RemoveCarbon(int amount) {
    carbon -= amount;
    }

    public void AddScrap(int amount) {
    scrap += amount;
    }

    public void RemoveScrap(int amount) {
    scrap -= amount;
    }
}
