using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    // the tile which will activate over selectable objects
    [SerializeField] public GameObject selectionTile;
    // the layers which can be selected
    [SerializeField] LayerMask selectableLayers;
    // reference to the selection text
    [SerializeField] TextMeshProUGUI selectionText;

    private RaycastHit2D objectHit;
    public RaycastHit2D ObjectHit { get { return objectHit; } }


    public Health selectedTileHealth;
    public GeneratorPowerHandler selectedTilePowerGeneratorHandler;
    public GameObject healthBar;
    public GameObject powerBar;
    public Image healthBarImage;
    public Image powerBarImage;

    
    // cursor sprites for each selectable layer
    [SerializeField] Sprite resourcesCursorSprite;
    [SerializeField] Sprite enemiesCursorSprite;
    [SerializeField] Sprite buildingsCursorSprite;
    [SerializeField] Sprite defaultCursorSprite;
    // cursor textures
    Texture2D resourcesCursor;
    Texture2D enemiesCursor;
    Texture2D buildingsCursor;
    Texture2D defaultCursor;

void Start()
    {
        // convert the sprites to textures
        resourcesCursor = resourcesCursorSprite.texture;
        enemiesCursor = enemiesCursorSprite.texture;
        buildingsCursor = buildingsCursorSprite.texture;
        defaultCursor = defaultCursorSprite.texture;
        // set the default cursor
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        // check if the mouse is hovering over an object on the Resources, Enemies, or Buildings layers
        objectHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, selectableLayers);
        if (objectHit.collider != null)
        {
            // if the mouse is hovering over an object, activate the Selection Tile
            selectionTile.SetActive(true);
            // if the mouse is hovering over an object, enable the selection text
            selectionText.enabled = true;
            // change the text for the currently selected object
            selectionText.text = objectHit.collider.gameObject.name;
            // store the layer index of the selected object
            selectionTile.layer = objectHit.collider.gameObject.layer;
            // move the Selection Tile to the center of the object
            selectionTile.transform.position = objectHit.collider.bounds.center;
            // get a reference to the health component on the hovered object
            selectedTileHealth = objectHit.collider.gameObject.GetComponent<Health>();
            if (selectedTileHealth != null)
            {
                healthBar.SetActive(true);
                // healthBarSlider.maxValue = selectedTileHealth.MaxHealth;
                healthBarImage.fillAmount = ((float)selectedTileHealth.CurrentHealth / (float)selectedTileHealth.MaxHealth);
            }
            selectedTilePowerGeneratorHandler = objectHit.collider.gameObject.GetComponent<GeneratorPowerHandler>();
            if (selectedTilePowerGeneratorHandler != null)
            {
                powerBar.SetActive(true);
                // healthBarSlider.maxValue = selectedTileHealth.MaxHealth;
                powerBarImage.fillAmount = ((float)selectedTilePowerGeneratorHandler.CurrentCarbon / (float)selectedTilePowerGeneratorHandler.MaxCarbon);
            }
            // check the layer of the object hit and set the cursor accordingly
            if (objectHit.collider.gameObject.layer == LayerMask.NameToLayer("Resources"))
            {
                Cursor.SetCursor(resourcesCursor, Vector2.zero, CursorMode.Auto);
            }
            else if (objectHit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                Cursor.SetCursor(enemiesCursor, Vector2.zero, CursorMode.Auto);
            }
            else if (objectHit.collider.gameObject.layer == LayerMask.NameToLayer("Buildings"))
            {
                Cursor.SetCursor(buildingsCursor, Vector2.zero, CursorMode.Auto);
            }

        }
        else
        {
            // if the mouse is not hovering over an object, deactivate the Selection Tile
            selectionTile.SetActive(false);
            // disable the selection text
            selectionText.enabled = false;
            // disable the healthbar 
            healthBar.SetActive(false);
            // disable the powerbar 
            powerBar.SetActive(false);
            // set cursor to default
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }

    }
}
