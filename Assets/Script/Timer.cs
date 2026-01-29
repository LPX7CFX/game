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
    public Categorychoose cc;

    void Start()
    {
        currentTime = 0;
        running = false;

        startButton.onClick.AddListener(StartTimer);
        stopButton.onClick.AddListener(Stoptimermanual);
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

    public void Stoptimermanual()
    {
        running = false;


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
        string typename = cc.type3k;
        Debug.Log("Savetest9");

        SaveManager.Instance.SubmitScore(
            LoginManager.CurrentUser,
            typename,
            finalTime
        );
        Debug.Log("Savetest10");
    }
}
