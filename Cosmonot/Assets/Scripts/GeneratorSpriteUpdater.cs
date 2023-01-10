using UnityEngine;

[RequireComponent(typeof(GeneratorPowerHandler))]
public class GeneratorSpriteUpdater : MonoBehaviour
{
    // A reference to the GeneratorPowerHandler component on the game object
    private GeneratorPowerHandler generatorPowerHandler;

    // The sprites to use for the different carbon levels
    [SerializeField] Sprite fullCarbonSprite, highCarbonSprite, midCarbonSprite, lowCarbonSprite;

    // Thresholds for the different carbon levels
    [SerializeField] float lowCarbonThreshold;
    [SerializeField] float midCarbonThreshold;

    // A reference to the Sprite renderer on the game object
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the GeneratorPowerHandler component on the game object
        generatorPowerHandler = GetComponent<GeneratorPowerHandler>();

        // Get the Sprite renderer component on the game object
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Set the initial sprite to the full carbon sprite
        spriteRenderer.sprite = fullCarbonSprite;
    }

    void Update()
    {
        // Change the sprite based on the current carbon level
        if (generatorPowerHandler.CurrentCarbon <= 0)
        {
            spriteRenderer.sprite = lowCarbonSprite;
        }
        else if (generatorPowerHandler.CurrentCarbon <= lowCarbonThreshold)
        {
            spriteRenderer.sprite = midCarbonSprite;
        }
        else if (generatorPowerHandler.CurrentCarbon <= midCarbonThreshold)
        {
            spriteRenderer.sprite = highCarbonSprite;
        }
        else
        {
            spriteRenderer.sprite = fullCarbonSprite;
        }
    }
}
