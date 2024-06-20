using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Update is called once per frame
    public void OpenLevelList()
    {
        SceneManager.LoadScene(1);
    }

    // Method to exit the game
    public void ExitGame()
    {
        Application.Quit();
        // This line is useful for confirming the exit in the editor
        Debug.Log("Game is exiting");
    }
}
