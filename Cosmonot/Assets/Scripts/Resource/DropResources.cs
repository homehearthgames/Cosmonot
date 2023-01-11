using UnityEngine;

[RequireComponent(typeof(Resources))]
public class DropResources : MonoBehaviour
{
    // declare a reference to the PlayerResources instance
    PlayerResources playerResources;

    // Reference to the Resources script
    Resources resources;

    private void Start() {        
        // Get a reference to the PlayerResources instance
        playerResources = PlayerResources.instance;

        resources = GetComponent<Resources>();
    }

    public void AddResources()
    {
        // Add the resources from the Resources script to the player's resources
        playerResources.AddCarbon(resources.Carbon);
        playerResources.AddScrap(resources.Scrap);
    }
}
