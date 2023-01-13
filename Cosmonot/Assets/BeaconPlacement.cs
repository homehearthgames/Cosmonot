using UnityEngine;
using UnityEngine.Tilemaps;

public class BeaconPlacement : MonoBehaviour
{
    public GameObject beaconBase;
    public GameObject beaconTransponder;
    public GameObject beaconReceiver;

    public Tilemap tilemap;

    void Start()
    {
        // Create an array of the objects to be placed
        GameObject[] objectsToPlace = { beaconBase, beaconTransponder, beaconReceiver };

        // Place the objects at random positions within the tilemap
        for (int i = 0; i < 3; i++)
        {
            // Get a random position within the tilemap's bounds
            float x = Random.Range(-30, 30);
            float y = Random.Range(-30, 30);
            Vector3 randomPos = new Vector3(x, y, 0);
            // Convert the random position from grid coordinates to world coordinates
            Vector3 worldPos = tilemap.CellToWorld(tilemap.WorldToCell(randomPos));
            // Choose a random object from the objectsToPlace array
            int objectIndex = Random.Range(0, objectsToPlace.Length);
            // Instantiate the object
            Instantiate(objectsToPlace[objectIndex], worldPos, Quaternion.identity);
        }
    }
}
