using System.Collections;
using UnityEngine;

public class TogglePOV : MonoBehaviour
{
    public GameObject mainCamera; // Assign this in the Inspector
    public Vector3 offsetFromPlayer1 = new Vector3(0, 0.62f, 0); // Offset from player for POV 1
    public Vector3 offsetFromPlayer2 = new Vector3(0, 3, -11);  // Offset from player for POV 2

    public GameObject player; // Reference to the Player GameObject

    void Start()
    {

        if (mainCamera != null && player != null)
        {
            // Initialize the camera position relative to the player
            mainCamera.transform.position = player.transform.position + offsetFromPlayer1;
        }
    }

    void Update()
    {
        // Check if F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle between two POV positions
            if (mainCamera != null && player != null)
            {
                mainCamera.transform.position = player.transform.position + offsetFromPlayer2;
            }
        }
    }
}
