using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // The max health of the game object
    [SerializeField] int maxHealth;
    public int MaxHealth { get { return maxHealth; } }

    // The current health of the game object
    [SerializeField] int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    // OnDeath event to be declared in the inspector
    public UnityEvent OnDeath;

    PlayerResources playerResources;

    void Start()
    {
        // Get a reference to the PlayerResources instance
        playerResources = PlayerResources.instance;

        // Set the current health to the maximum health at the start
        currentHealth = maxHealth;
    }

    private void Update() {
        // If the current health is less than or equal to 0, call the Die function
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int weaponValue)
    {
        // Reduce the current health by the damage taken
        currentHealth -= weaponValue;
        // If the current health is less than or equal to 0, call the Die function
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Repair(int weaponValue)
    {
        // If the current health is less than or equal to 0, call the Die function
        if (currentHealth != maxHealth)
        {
            if (playerResources.Scrap != 0)
            {
                // Add the specified amount of carbon to currentCarbon
                currentHealth += weaponValue * 2;
                // TODO: Subtract the resources from the player resource pool (lets scale this later)
                playerResources.RemoveScrap(Mathf.RoundToInt(weaponValue / 2) + 1);
            }
        }
    }

    void Die()
    {
        // Call the OnDeath event
        OnDeath.Invoke();
        gameObject.SetActive(false);
    }
}
