/* File Seek.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 06 Sep 2022
 */

using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 0f;


    public override Vector3 GetForce()
    {
            Vector3 force = Vector3.zero;

            if (target == null) return force;
            Vector3 desiredForce = target.position - transform.position;

            // Check if the direction is valid
            if (desiredForce.magnitude > stoppingDistance)
            {
                // Calculate force
                desiredForce = desiredForce.normalized * weighting;
                force = desiredForce - owner.velocity;
            }
            return force;
        }

    }
