using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float yVelocity;
    [SerializeField] private float jumpSpeed;

    [Header("Character properties")]
    [SerializeField] private float speed = 5;
    public float turnSpeed = 2;
    [SerializeField] private float jumpHeight = 1;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();

        jumpSpeed = Mathf.Sqrt(jumpHeight * 2f * 9.8f * 0.02f * 0.02f);//sets jump speed to get correct jump height
    }

    // Update is called once per frame
    void Update()
    {
        //put some pausing code here maybe

        //moving function
        Move();
        //rotation function
        Rotate();
    }

    private void Move()
    {
        //wasd controls
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move *= speed * Time.deltaTime;

        //jump if on ground and pressing space
        if (_characterController.isGrounded)
        {
            yVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }//apply gravity
        yVelocity += GameManager.gravityAcceleration;
        move.y = yVelocity;

        //apply rotation
        move = transform.TransformDirection(move);
        
        //apply move
        _characterController.Move(move);
    }

    private void Rotate()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * turnSpeed, 0);
    }
}
