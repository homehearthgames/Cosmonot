using UnityEngine;
using UnityEngine.UI;

public class DirectionalArrowController : MonoBehaviour
{
    public Transform ship;
    public Image arrowImage;
    public RectTransform arrowRect;
    public float edgeBuffer = 10f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (IsObjectOffScreen(ship))
        {
            arrowImage.enabled = true;
            Vector2 shipScreenPos = mainCamera.WorldToScreenPoint(ship.position);
            Vector2 clampedPosition = ClampPositionToScreen(shipScreenPos, arrowRect, edgeBuffer);
            arrowRect.position = clampedPosition;
            arrowRect.rotation = Quaternion.Euler(0f, 0f, AngleFromObjectToCenter(shipScreenPos));
        }
        else
        {
            arrowImage.enabled = false;
        }
    }

    private bool IsObjectOffScreen(Transform obj)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(obj.position);
        return screenPos.z <= 0 || screenPos.x <= 0 || screenPos.x >= Screen.width || screenPos.y <= 0 || screenPos.y >= Screen.height;
    }
    
    private float AngleFromObjectToCenter(Vector2 objScreenPos)
    {
        Vector2 centerScreenPos = new Vector2(Screen.width / 2, Screen.height / 2);
        return Mathf.Atan2(objScreenPos.y - centerScreenPos.y, objScreenPos.x - centerScreenPos.x) * Mathf.Rad2Deg - 90;
    }
    private Vector2 ClampPositionToScreen(Vector2 objScreenPos, RectTransform rectTransform, float buffer)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        float rectWidth = corners[2].x - corners[0].x;
        float rectHeight = corners[1].y - corners[0].y;
        float xMin = rectWidth / 2 + buffer;
        float xMax = Screen.width - rectWidth / 2 - buffer;
        float yMin = rectHeight / 2 + buffer / 2;
        float yMax = Screen.height - rectHeight / 2 - buffer / 2;
        return new Vector2(Mathf.Clamp(objScreenPos.x, xMin, xMax), Mathf.Clamp(objScreenPos.y, yMin, yMax));
    }
}
