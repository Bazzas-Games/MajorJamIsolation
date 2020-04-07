using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsRunning = true;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // When "Escape" is pressed, Resume game if Pause. Pause game if not.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsRunning)
            {
               Resume(); 
			} else
            {
               Pause();
			}
        } 
    }

    // Bring Down Pause Mene.
    // Unfreeze Time in Game.
    // Change GameIsRunning to true.
    public void Resume()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsRunning = true;
	}

    // Bring up Pause Menu.
    // Freeze Time in Game.
    // Change GameIsRunning to false.
    void Pause()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsRunning = false;
	}

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
	}

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
	}
}
