/* File Wander.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 06 Sep 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviour
{
    public float offset = 1.0f;
    public float radius = 1.0f;
    public float jitter = 0.2f;

    private Vector3 targetDir;
    private Vector3 randomDir;

    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero; // Set force to zero
        float randX = Random.Range(0, 0x7FFF) - (0x7FFF * 0.5f);
        float randZ = Random.Range(0, 0x7FFF) - (0x7FFF * 0.5f);

        #region Calculate Random Direction
        randomDir = new Vector3(randX, 0, randZ);   // Create the random direction vector
        randomDir = randomDir.normalized;           // Normalize the random direction
        randomDir *= jitter;                        // Multiply randomDir by jitter
        #endregion

        #region Calculate Target Direction
        targetDir += randomDir;                     // Append target direction with random directon
        targetDir = targetDir.normalized;           // Normalize the target direction
        targetDir *= radius;                        // Multiply target direction by the radius
        #endregion

        // Calculate seek position using targetDir
        Vector3 seekPos = transform.position + targetDir;
        seekPos += transform.forward * offset;

        #region Wander
        Vector3 direction = seekPos - transform.position;   // Calculate direction
        // Is direction valid? (not zero)
        if (direction.magnitude > 0)
        {
            // Calculate force
            Vector3 desiredForce = direction.normalized * weighting;
            force = desiredForce - owner.velocity;
        }
        #endregion

        return force;
    }
}
