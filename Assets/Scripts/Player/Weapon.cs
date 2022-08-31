/* File Weapon.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 30 August 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] float weaponRange; // Range setting for weapon
    
    Camera mainCam;
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
        Ray shootingRay = new Ray(mainCam.transform.position, Vector3.forward);

        if (Physics.Raycast(shootingRay, out hit, weaponRange))
        {
            // Debug.DrawRay(mainCam.transform.position, Vector3.forward * weaponRange, Color.green);
            if (hit.transform.tag.Equals("Enemy"))
            {
                // TODO: add enemy destroy stuff here
                Debug.Log("Enemy hit!");
            }

            else
            {
                Debug.Log("No objects hit!");
            }
        }
    }
}
