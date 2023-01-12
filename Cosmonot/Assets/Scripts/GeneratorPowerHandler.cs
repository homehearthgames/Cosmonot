using System.Collections;
using UnityEngine;

public class GeneratorPowerHandler : MonoBehaviour
{
    // int variables for the current and maximum amount of carbon
    [SerializeField] int currentCarbon;
    public int CurrentCarbon { get { return currentCarbon; } }

    [SerializeField] int maxCarbon;
    public int MaxCarbon { get { return maxCarbon; } }

    // X units of carbon to subtract every Y seconds
    public int carbonSubtractAmount = 10;
    public float carbonSubtractInterval = 5.0f;

    // declare a reference to the PlayerResources script
    PlayerResources playerResources;

    // Coroutine reference
    private Coroutine carbonSubtractCoroutine;

    bool generatorRunning;

    public SignalEmitter signalEmitter;

    void Start()
    {
        // Get a reference to the PlayerResources instance
        playerResources = PlayerResources.instance;

        currentCarbon = maxCarbon;
        // Start the coroutine to subtract carbon from currentCarbon every Y seconds
        carbonSubtractCoroutine = StartCoroutine(SubtractCarbon());
    }

    IEnumerator SubtractCarbon()
    {
        while (true)
        {
            generatorRunning = true;
            // Wait for Y seconds before subtracting carbon again
            yield return new WaitForSeconds(carbonSubtractInterval);

            // If currentCarbon is 0, stop the coroutine and return
            if (currentCarbon == 0) {
                signalEmitter.gameObject.SetActive(false);
                generatorRunning = false;
                yield break;
            }

            // Subtract X carbon from currentCarbon of generator carbon pool
            currentCarbon = Mathf.Max(0, currentCarbon - carbonSubtractAmount);
        }
            
    }

    public void AddCarbon(int weaponValue)
    {
        if (playerResources.Carbon != 0)
        {
            // Add the specified amount of carbon to currentCarbon
            currentCarbon += weaponValue * 2;
            // TODO: Subtract the resources from the player resource pool (lets scale this later)
            playerResources.RemoveCarbon(Mathf.RoundToInt(weaponValue / 2) + 1);

            // If the coroutine is not running and currentCarbon is greater than 0, start the coroutine
            if (!generatorRunning && currentCarbon > 0)
            {
                carbonSubtractCoroutine = StartCoroutine(SubtractCarbon());
                signalEmitter.gameObject.SetActive(true);
            }
        }

    }
}
