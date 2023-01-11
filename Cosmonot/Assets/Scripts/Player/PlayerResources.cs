using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour 
{    
    // Singleton instance of the PlayerResources script
    public static PlayerResources instance;


    [SerializeField] int carbon;    
    public int Carbon {
        get { return carbon; }
        set { carbon = value; }
    }
    [SerializeField] int scrap;
    public int Scrap {
        get { return scrap; }
        set { scrap = value; }
    }

    [SerializeField] TextMeshProUGUI carbonText;
    [SerializeField] TextMeshProUGUI scrapText;

    void Awake()
    {
        // Set the instance to this script
        instance = this;
    }
    
    private void Update() {
        carbonText.text = "Carbon: " + carbon.ToString();
        scrapText.text = "Scrap: " + scrap.ToString();
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
