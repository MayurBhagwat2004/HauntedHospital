using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Resume()
    {
            if(isPaused)
            {
                Time.timeScale = 1f;
                isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
    
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void Pause()
    {
        
            Cursor.lockState = CursorLockMode.None;
            if(!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
            }
        

    }
}