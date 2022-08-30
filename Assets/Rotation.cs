using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Quarternions are rotations
        //Apply the rotation to a vector
        Quaternion example = Quaternion.identity;

        Vector3 thisDirection = new Vector3();

        Vector3 newDirection = example * thisDirection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
