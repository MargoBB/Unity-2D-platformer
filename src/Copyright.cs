using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Copyright : MonoBehaviour
{
    public void OpenLevelList()
    {
        SceneManager.LoadScene(5);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
}
