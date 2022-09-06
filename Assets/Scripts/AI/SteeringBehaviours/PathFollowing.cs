/* File PathFollowing.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 06 Sep 2022
 */
 
using UnityEngine;
using UnityEngine.AI;


public class PathFollowing : SteeringBehaviour
{
    public Transform target;                                                  
    public float nodeRadius = 0.1f;
    public float targetRadius = 3f;
    public bool isAtTarget = false;
    public int currentNode = 0;

    private NavMeshAgent nav;
    private NavMeshPath path;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    Vector3 Seek(Vector3 target)
    {
        Vector3 force = Vector3.zero;
        float distanceToTarget = Vector3.Distance(target, transform.position);
        float radius = isAtTarget ? targetRadius : nodeRadius;
            
        if (distanceToTarget > radius)
        {
            Vector3 direction = (target - transform.position).normalized * weighting;      // Apply weighting to force
            force = direction - owner.velocity;                                 // Apply desired force to force (removing current owner's velocity)
        }

        return force;                                                           // Return force
    }

    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        // Is there not a target?
        if (!target)
        {
            return force;
        }

        // Calculate path using the nav agent
        if (nav.CalculatePath(target.position, path))
        {
            // Is the path finished calculating?
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                Vector3[] corners = path.corners;
                // Are there any corners in the path?
                if (corners.Length > 0)
                {
                    int lastIndex = corners.Length - 1;
                    // Is currentNode at the end of the list?
                    if(currentNode >= corners.Length)
                    {
                        currentNode = lastIndex;
                    }
                    // Getthe current corner position
                    Vector3 currentPos = corners[currentNode];
                    float distance = Vector3.Distance(transform.position, currentPos);  // Get the distance to current pos
                    // Is the distance with nodeRadius
                    if (distance <= nodeRadius)
                    {
                        currentNode++; // Move to the next node
                    }
                    // Is the agent at the target?
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    isAtTarget = distanceToTarget <= targetRadius;
                    force = Seek(currentPos); // Seek towards current node's position
                }
            }
        }

        return force;
    }

    #region NOTES
    int SumOf(params int[] values)
    {
        int result = 0;
        foreach (var n in values)
        {
            result += n;
        }
        return result;
    }
    #endregion
}
