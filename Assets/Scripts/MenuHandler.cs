#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler instance { get; set; }
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(instance);
        }
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SendPlayerToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetQuality(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel,true);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
