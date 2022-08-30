using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;

    public static bool isPaused; //public for testing purposes, not necessarily needed to be public, static in case other parts need to use it

    void Start()
    {
        UnPaused();
    }

    public void Paused() //when paused is triggered
    {
        pauseMenuCanvas.SetActive(true);
        //stop our time
        Time.timeScale = 0;
        //free our cursor
        Cursor.lockState = CursorLockMode.Confined;
        //see our cursor
        Cursor.visible = true;
    }
    public void UnPaused() //when unpaused is triggered
    {
        //unpause our game if attached to a button...doesn't matter if it's an ESC toggle
        //isPaused = false;
        pauseMenuCanvas.SetActive(false);
        //start time
        Time.timeScale = 1;
        //lock our cursor
        Cursor.lockState = CursorLockMode.Locked;
        //hide our cursor
        Cursor.visible = false;
    }
    private void Update()
    {
        //GetKeyDown    On Press
        //GetKey        While Held
        //GetKeyUp      On Release
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuCanvas.SetActive(true);
            Paused();
        }
    }

    public void ChangeSceneViaIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ChangeSceneViaName(string sceneName) // This works the same as above but with names instead of scene numbers (eg. 1)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitTheGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //don't need UnityEditor at the beginning since UnityEditor is here. Easier to put up top 
        #endif
        Application.Quit();
    }
}
