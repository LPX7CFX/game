using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using NUnit.Framework.Internal;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;
    float currenttime;
    bool stopwatch = false;
    public Button start;
    public Button stop;
    private LeaderBoardManager leaderBoard;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenttime = 0;
        start.onClick.AddListener(starttime);

        stop.onClick.AddListener(stoptimemanual);
    }

    // Update is called once per frame
    public void Update()
    {
        if (stopwatch == true)
        {
            currenttime = currenttime + Time.deltaTime;

        }
        TimeSpan time = TimeSpan.FromSeconds(currenttime);
        text.text = time.Seconds.ToString();
        
    }

    void starttime()
    {
        stopwatch = true;

    }
    void stoptimemanual()
    {
        stopwatch = false;

    }
    void stoptimeauto()
    {
        stopwatch = false;

    }
}
