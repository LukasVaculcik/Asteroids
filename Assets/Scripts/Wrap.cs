using UnityEngine;

// Wrap around if thing goes off the screen
public class Wrap : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        // Convert world point to Viewport so its normalized to 0 to 1 range.
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // If object is moved out of the viewport wrap to opposite side.
        Vector3 moveAdjustment = Vector3.zero;
        if (viewportPosition.x < 0) {
            moveAdjustment.x += 1;
        } else if (viewportPosition.x > 1) {
            moveAdjustment.x -= 1;
        } else if (viewportPosition.y < 0) {
            moveAdjustment.y += 1;
        } else if (viewportPosition.y > 1) {
            moveAdjustment.y -= 1;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjustment);
    }
}
