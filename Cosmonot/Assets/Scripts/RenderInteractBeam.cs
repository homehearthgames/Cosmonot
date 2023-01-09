using UnityEngine;

public class RenderInteractBeam : MonoBehaviour
{
    // the line renderer that will be used to draw the beam
    LineRenderer lineRenderer;
    // a reference to the PlayerSelection script
    PlayerSelection playerSelection;

    void Start()
    {
        // get a reference to the line renderer component
        lineRenderer = GetComponent<LineRenderer>();
        // get a reference to the PlayerSelection script
        playerSelection = FindObjectOfType<PlayerSelection>();
    }

    void Update()
    {
        // check if the PlayerSelection script is present in the scene
        if (playerSelection != null)
        {
            // check if the mouse is hovering over an object
            if (playerSelection.selectionTile.activeSelf)
            {
                // set the start and end positions of the beam
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, playerSelection.selectionTile.transform.position);
                // enable the line renderer
                lineRenderer.enabled = true;
            }
            else
            {
                // disable the line renderer
                lineRenderer.enabled = false;
            }
        }
    }
}
