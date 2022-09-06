/* File Weapon.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 30 August 2022
 */

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] float weaponRange; // Range setting for weapon
    
    Camera mainCam;
    public HeatLevel heatLevel; // Reference to the script
    void Awake()
    {
        // Grab player's camera proxy and make it a variable
        if (mainCam == null)
            mainCam = Camera.main;
    }
    void Update()
    {
        // Check if the left mouse button is pressed, then make the raycast
        if (Input.GetMouseButtonDown(0))
            HandleRaycast();
    }

    private void HandleRaycast()
    {
        RaycastHit hit;
        Ray shootingRay = new Ray(mainCam.transform.position, transform.forward);

        if (Physics.Raycast(shootingRay, out hit, weaponRange))
        {
            // Debug.DrawRay(mainCam.transform.position, transform.forward * weaponRange, Color.green);
            if (hit.transform.tag.Equals("Enemy"))
            {
                Destroy(hit.transform.gameObject); // Destroy the instance
                heatLevel.HeatLevelUp(); // Call the relevant method from the script
                Debug.Log("Enemy hit!");
                // TODO: Ammo display and subtract when fired
            }

            else
            {
                Debug.Log("No objects hit!");
            }
        }
    }
}
