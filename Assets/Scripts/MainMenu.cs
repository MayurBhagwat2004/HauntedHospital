using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public bool stateChanged;
    public AudioClip homeAudioClip;
    private void Start()
    {
        if (SoundHandler.instance && SceneManager.GetActiveScene().buildIndex==0)
        {
            SoundHandler.instance.audioClip = homeAudioClip;

        }
       
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        stateChanged = true;
    }



    public void QuitGame()
    {
        Application.Quit();
    }

   
}
