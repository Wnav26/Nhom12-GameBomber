using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player4SceneURF : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("4PlayerURF");
    }
}
