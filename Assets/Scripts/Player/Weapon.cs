/* File Weapon.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 30 August 2022
 */

using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] float weaponRange; // Range setting for weapon
    
    GameObject mainCam;
    //public GameObject shatter;

    
    public HeatLevel heatLevel; // Reference to the script
    NavMeshUpdate navMeshUpdate;
    public int ammo;
    public Text ammoDisplay;
    private bool isFiring;
    
    void Awake()
    {
        // Grab player's camera proxy and make it a variable
        if (mainCam == null)
            mainCam = Camera.main.gameObject;
        if (navMeshUpdate == null)
            navMeshUpdate = gameObject.AddComponent<NavMeshUpdate>();
    }
    void Update()
    {
        ammoDisplay.text = ammo.ToString();
        
        // Check if the left mouse button is pressed, then make the raycast
        if (Input.GetMouseButtonDown(0) && !isFiring && ammo > 0)
            {
                isFiring = true;
                HandleRaycast();
                ammo--;
                isFiring = false;
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
                /* Need to create conditional so that the NavMesh
                 * is rebaked after certain glass objects are shattered - enemies can no long cross */
                // navMeshUpdate.RebakeNavMesh();
                Debug.Log("Glass hit!");
            }

            else
            {
                Debug.Log("No objects hit!");
            }
        }
    }


}
