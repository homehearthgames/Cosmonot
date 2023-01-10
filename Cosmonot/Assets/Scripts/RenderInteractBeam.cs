using UnityEngine;

public class RenderInteractBeam : MonoBehaviour
{
    // the line renderer that will be used to draw the beam
    LineRenderer lineRenderer;
    // a reference to the PlayerSelection script
    PlayerSelection playerSelection;
    // check if the beam is rendering
    bool isRendering = false;    
    public bool IsRendering {
        get { return isRendering; }
        set { isRendering = value; }
    }


    void Start()
    {
        // get a reference to the line renderer component
        lineRenderer = GetComponent<LineRenderer>();
        // get a reference to the PlayerSelection script
        playerSelection = FindObjectOfType<PlayerSelection>();
    }

    void Update()
    {
        RenderBeam();
    }

void RenderBeam()
{
    if(Input.GetMouseButton(0)){
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
                
                int layer = playerSelection.selectionTile.layer;

                // set the color of the line based on the layer
                if (layer == LayerMask.NameToLayer("Resources"))
                {
                    lineRenderer.material.color = Color.cyan;
                }
                else if (layer == LayerMask.NameToLayer("Enemies"))
                {
                    lineRenderer.material.color = Color.red;
                }
                else if (layer == LayerMask.NameToLayer("Buildings"))
                {
                    lineRenderer.material.color = Color.green;
                }
            }
            else
            { 
                // disable the line renderer
                lineRenderer.enabled = false;
            }
        }
    }else{
        lineRenderer.enabled = false;
    }
}


    
}
