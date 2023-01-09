using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    // reference to the PlayerSelection script
    public PlayerSelection playerSelection;

    Vector3 direction;

    void Update()
    {
        FlipSprite();
        // if the player is hovering over a selectable object, point the weapon toward it
        if (playerSelection.selectionTile.activeSelf)
        {
            Vector3 direction = (playerSelection.selectionTile.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        // if the player is not hovering over a selectable object, point the weapon toward the cursor
        else
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0;
            direction = (cursorPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FlipSprite()
    {
        Vector2 scale = transform.localScale;
        if (playerSelection.selectionTile.activeSelf)
        {
            Vector3 direction = (playerSelection.selectionTile.transform.position - transform.position).normalized;
            if (direction.x < 0)
            {
                scale.y = -1;
            }
            else if (direction.x > 0)
            {
                scale.y = 1;
            }
        }
        else
        {
            if (direction.x < 0)
            {
                scale.y = -1;
            }
            else if (direction.x > 0)
            {
                scale.y = 1;
            }
        }
        transform.localScale = scale;
    }

}
