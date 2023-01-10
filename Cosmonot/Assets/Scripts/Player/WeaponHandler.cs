using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    PlayerSelection playerSelection;
    // WEAPON INFO
    [SerializeField] float fireRate = 0.5f;
    float lastShotTime = 0;

    [SerializeField] int weaponValue;
    public int WeaponValue { get { return weaponValue; } }

    [SerializeField] RenderInteractBeam renderInteractBeam;

    // Start is called before the first frame update
    void Start()
    {
        playerSelection = GetComponentInParent<PlayerSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the PlayerSelection script is present in the scene
        if (playerSelection != null)
        {
            // check if the mouse is hovering over an object
            if (playerSelection.selectionTile.activeSelf)
            {
                // get the layer of the selected object
                int layer = playerSelection.selectionTile.layer;
                if (Input.GetMouseButton(0) && Time.time > lastShotTime + fireRate)
                {

                    // interact with the object based on the layer
                    if (layer == LayerMask.NameToLayer("Resources"))
                    {
                        playerSelection.selectedTileHealth.TakeDamage(weaponValue);
                        Debug.Log("I clicked on a Resource! It's current health is " + playerSelection.selectedTileHealth.CurrentHealth);
                    }
                    else if (layer == LayerMask.NameToLayer("Enemies"))
                    {
                        playerSelection.selectedTileHealth.TakeDamage(weaponValue);
                        Debug.Log("I clicked on an Enemy! It's current health is " + playerSelection.selectedTileHealth.CurrentHealth);
                    }
                    else if (layer == LayerMask.NameToLayer("Buildings"))
                    {
                        GeneratorPowerHandler generatorPowerHandler = playerSelection.ObjectHit.collider.gameObject.GetComponent<GeneratorPowerHandler>();
                        if (playerSelection.selectedTileHealth != null && playerSelection.selectedTileHealth.CurrentHealth < playerSelection.selectedTileHealth.MaxHealth)
                        {
                            playerSelection.selectedTileHealth.Repair(weaponValue);
                            Debug.Log("I clicked on a Building! It's current health is " + playerSelection.selectedTileHealth.CurrentHealth);
                        }
                        if (generatorPowerHandler != null && generatorPowerHandler.CurrentCarbon <= generatorPowerHandler.MaxCarbon)
                        {
                            generatorPowerHandler.AddCarbon(weaponValue);
                            Debug.Log("I clicked on a Generator! It's current power is " + generatorPowerHandler.CurrentCarbon);
                        }
                    }
                    lastShotTime = Time.time;
                }
            }
        }
    }
}
