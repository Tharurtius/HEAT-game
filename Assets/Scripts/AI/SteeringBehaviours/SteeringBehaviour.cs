/* File SteeringBehaviour.cs
 * Author: Simon Vannarath (Bush Turkey Studios)
 * Created: 06 Sep 2022
 */
 
using UnityEngine;

[RequireComponent(typeof(AIAgent))]
public class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 7.5f;
    [HideInInspector]
    public AIAgent owner;

    protected virtual void Awake()
    {
        owner = GetComponent<AIAgent>();
    }

    public virtual Vector3 GetForce()
    {
        return Vector3.zero;
    }
}
