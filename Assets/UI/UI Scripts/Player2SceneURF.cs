using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2SceneURF : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("2PlayerURF");
    }
}
