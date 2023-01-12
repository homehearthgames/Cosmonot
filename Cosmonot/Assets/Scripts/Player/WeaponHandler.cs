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
                    }
                    else if (layer == LayerMask.NameToLayer("Enemies"))
                    {
                        playerSelection.selectedTileHealth.TakeDamage(weaponValue);
                    }
                    else if (layer == LayerMask.NameToLayer("Buildings"))
                    {
                        GeneratorPowerHandler generatorPowerHandler = playerSelection.ObjectHit.collider.gameObject.GetComponent<GeneratorPowerHandler>();
                        if (playerSelection.selectedTileHealth != null && playerSelection.selectedTileHealth.CurrentHealth < playerSelection.selectedTileHealth.MaxHealth)
                        {
                            playerSelection.selectedTileHealth.Repair(weaponValue);
                        }
                        if (generatorPowerHandler != null && generatorPowerHandler.CurrentCarbon <= generatorPowerHandler.MaxCarbon)
                        {
                            generatorPowerHandler.AddCarbon(weaponValue);
                        }
                    }
                    lastShotTime = Time.time;
                }
            }
        }
    }
}
