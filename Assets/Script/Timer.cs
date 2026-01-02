using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using NUnit.Framework;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Button startButton;
    public Button stopButton;

    public float currentTime;
    private bool running;
    bool hasSaved = false;

    void Start()
    {
        currentTime = 0;
        running = false;

        startButton.onClick.AddListener(StartTimer);
        stopButton.onClick.AddListener(StopTimer);
    }

    void Update()
    {
        if (!running) return;

        currentTime += Time.deltaTime;

        int seconds = Mathf.FloorToInt(currentTime);
        timerText.text = seconds.ToString();
    }

    void StartTimer()
    {
        currentTime = 0;
        running = true;
        hasSaved = false;
    }

    public void StopTimer()
    {   

        Debug.Log("Savetest6");
         if (hasSaved) return;
            hasSaved = true;
            Debug.Log("Savetest7");
        running = false;
        Debug.Log("Savetest8");

        int finalTime = Mathf.FloorToInt(currentTime);
        Debug.Log("Savetest9");

        SaveManager.Instance.SubmitScore(
            LoginManager.CurrentUser,
            finalTime
        );
        Debug.Log("Savetest10");
    }
}
