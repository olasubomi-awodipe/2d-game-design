using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        // loads the next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GetOptions()
    {
        // should load the options menu
        // SceneManager.LoadScene("OptionsMenu");
    }

    public void GetCredits()
    {
        // should load the credits menu
        // SceneManager.LoadScene("CreditsMenu")
    }
    public void QuitGame ()
    {
        // a debug statement to check if the operation was performed
        Debug.Log("The game was quitted!");
        // quits the game
        Application.Quit();
    }

    
}
