/* File AIAgent.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 06 Sep 2022
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float maxDistance = 5;
    public bool updatePosition = false;
    public bool updateRotation = false;

    [HideInInspector]
    public Vector3 velocity;
    
    private Vector3 force;
    private List<SteeringBehaviour> behaviours;
    private NavMeshAgent nav;

    private void Awake() 
    {
        nav = GetComponent<NavMeshAgent>();
        behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
    }
    
    private void ComputeForces()
    {
        force = Vector3.zero;
        for (int i = 0; i < behaviours.Count; i++)
        {
            SteeringBehaviour behaviour = behaviours[i];
            if (!behaviour.isActiveAndEnabled)
            {
                continue;
            }

            force += behaviour.GetForce() * behaviour.weighting;
            if (force.magnitude > maxSpeed)
            {
                force = force.normalized * maxSpeed;
                break;
            }
        }
    }

    private void ApplyVelocity()
    {
        velocity += force * Time.deltaTime;
        nav.speed = velocity.magnitude;
        if (velocity.magnitude > 0 && nav.updatePosition)
        {
            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
            
            Vector3 pos = transform.position + velocity;
            NavMeshHit navHit;

            if (NavMesh.SamplePosition(pos, out navHit, maxDistance, -1))
            {
                nav.SetDestination(navHit.position);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        nav.updatePosition = updatePosition;
        nav.updateRotation = updateRotation;
        ComputeForces();
        ApplyVelocity();
    }
}
