using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gravity = 5;
    public static float gravityAcceleration;
 
    // Start is called before the first frame update
    void Start()
    {
        //60fps
        Application.targetFrameRate = 60;
        ToggleCursorMode(false);
    }

    // Update is called once per frame
    void Update()
    {
        //gravity calcs
        gravityAcceleration = gravity * Time.deltaTime * Time.deltaTime;
    }

    //set to true to lock cursor and make it invisible, set to false to make cursor visible and unlocked
    private void ToggleCursorMode(bool state)
    {
        Cursor.visible = !state;
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
