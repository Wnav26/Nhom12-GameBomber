using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI timeText;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        float loTime;
        loTime = Time.time - startTime;
        int minutes = ((int)loTime / 60);
        int sec = ((int)loTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, sec);
    }
}
