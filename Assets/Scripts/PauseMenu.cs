using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // When "Escape" is pressed, Resume game if Pause. Pause game if not.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
               Resume(); 
			} else
            {
               Pause();
			}
        } 
    }

    // Bring Down Pause Menu.
    // Unfreeze Time in Game.
    // Change GameIsPaused to false.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
	}

    // Bring up Pause Menu.
    // Freeze Time in Game.
    // Change GameIsPaused to true.
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
	}

    public void LoadMenu()
    {
        SceneManager.UnloadSceneAsync("Level1");
        SceneManager.LoadSceneAsync("Menu");
	}

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
	}
}
