using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // If the reset button is pressed
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            // Reload the currently active scene
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
