using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    // Update is called once per frame
    public void OpenLevelList()
    {
        SceneManager.LoadScene(2);
    }
}
