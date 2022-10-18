/* File Weapon.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 30 August 2022
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] float weaponRange; // Range setting for weapon
    
    GameObject mainCam;
    public NavMeshUpdate navMeshUpdate;

    
    public HeatLevel heatLevel; // Reference to the script
    public int ammo;
    public Text ammoDisplay;
    private bool isReloading = false;
    
    void Awake()
    {
        // Grab player's camera proxy and make it a variable
        if (mainCam == null)
            mainCam = Camera.main.gameObject;

    }
    void Update()
    {
        if(!isReloading)
        {
            ammoDisplay.text = ammo.ToString();
        }
        else
        {
            ammoDisplay.text = "Reloading";
        }

        
        // Check if the left mouse button is pressed, then make the raycast
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
                HandleRaycast();
                ammo--;
                //navMeshUpdate.MeshUpdate();
        }
        //if out of ammo and not currently reloading
        else if (Input.GetMouseButtonDown(0) && !isReloading && ammo == 0)
        {
            StartCoroutine(Reload());
        }

    }

    private void HandleRaycast()
    {
        RaycastHit hit;
        Ray shootingRay = new Ray(mainCam.transform.position, mainCam.transform.forward);

        if (Physics.Raycast(shootingRay, out hit, weaponRange))
        {
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * weaponRange, Color.green);
            if (hit.transform.tag.Equals("Enemy"))
            {
                Destroy(hit.transform.gameObject); // Destroy the instance
                heatLevel.HeatLevelUp(); // Call the relevant method from the script
                Debug.Log("Enemy hit!");
            }

            else if (hit.transform.tag.Equals("Glass"))
            {
                hit.transform.parent.GetComponent<Shatter>().ShatterGlass();// Destroy the glass
                Debug.Log("Glass hit!");
            }

            else
            {
                Debug.Log("No objects hit!");
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(3f);
        ammo = 10;
        isReloading = false;
    }
}
