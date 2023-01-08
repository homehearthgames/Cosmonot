using UnityEngine;
using TMPro;

public class PlayerSelection : MonoBehaviour
{
    // the tile which will activate over selectable objects
    [SerializeField] GameObject selectionTile;
    // the layers which can be selected
    [SerializeField] LayerMask selectableLayers;
    // reference to the selection text
    [SerializeField] TextMeshProUGUI selectionText;

    void Update()
    {
        // check if the mouse is hovering over an object on the Resources, Enemies, or Buildings layers
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, selectableLayers);
        if (hit.collider != null)
        {
            // if the mouse is hovering over an object, activate the Selection Tile
            selectionTile.SetActive(true);
            // if the mouse is hovering over an object, enable the selection text
            selectionText.enabled = true;
            // change the text for the currently selected object
            selectionText.text = hit.collider.gameObject.name;
            // move the Selection Tile to the center of the object
            selectionTile.transform.position = hit.collider.bounds.center;
        }
        else
        {
            // if the mouse is not hovering over an object, deactivate the Selection Tile
            selectionTile.SetActive(false);
            // disable the selection text
            selectionText.enabled = false;
        }
    }
}
