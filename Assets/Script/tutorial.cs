using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [Header("Tutorial Scene")]
    [SerializeField] private GameObject tutorialScene; // Tutorial Scene หรือ Canvas ของ Tutorial
    [SerializeField] private string modeSceneName = "Mode Scene"; // ชื่อ Scene Mode Scene

    [Header("Tutorial Steps (Canvas/GameObjects)")]
    [SerializeField] private GameObject[] tutorialSteps = new GameObject[5];

    [Header("Control Buttons")]
    [SerializeField] private Button playButton; // ปุ่ม Play ใน Start Scene
    [SerializeField] private Button nextButton; // ปุ่ม Next ใน Tutorial Scene
    [SerializeField] private Button modeSceneReplayButton; // ปุ่ม Replay Tutorial ใน Mode Scene

    private int currentTutorialStep = 0;
    private static bool hasShownFirstTimeOnModeScene = false;
    private static bool isFirstGameStart = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ปิด Tutorial Scene ตั้งแต่เริ่มต้น
        if (tutorialScene != null)
            tutorialScene.SetActive(false);

        // ลงทะเบียน listener สำหรับการโหลด Scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // ยกเลิก listener เมื่อ destroy
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // เรียกอัตโนมัติเมื่อโหลด Scene
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ตรวจสอบว่าเข้า Mode Scene ในครั้งแรกของการเปิดเกมหรือไม่
        if (scene.name == modeSceneName && isFirstGameStart && !hasShownFirstTimeOnModeScene)
        {
            isFirstGameStart = false;
            ShowTutorialFirstTime();
        }
    }

    private void Start()
    {
        // ลงทะเบียน listener สำหรับปุ่ม Play ใน Start Scene
        if (playButton != null)
            playButton.onClick.AddListener(ShowTutorialFirstTime);

        // ลงทะเบียน listener สำหรับปุ่ม Next
        if (nextButton != null)
            nextButton.onClick.AddListener(GoToNextTutorialStep);

        // ลงทะเบียน listener สำหรับปุ่ม Replay Tutorial ใน Mode Scene
        if (modeSceneReplayButton != null)
            modeSceneReplayButton.onClick.AddListener(ReplayTutorial);
    }

    /// <summary>
    /// เรียกตอนเข้า Mode Scene ครั้งแรก (นับจากตอนเปิดเกม)
    /// โดยเช่น: Training Button ใน Mode Scene เรียกฟังก์ชันนี้เมื่อกด
    /// </summary>
    public void ShowTutorialFirstTime()
    {
        if (!hasShownFirstTimeOnModeScene)
        {
            OpenTutorialScene();
            hasShownFirstTimeOnModeScene = true;
        }
    }

    /// <summary>
    /// เรียกจากปุ่ม Replay Tutorial ใน Mode Scene
    /// เปิด Tutorial Scene ใหม่ตั้งแต่ step 0
    /// </summary>
    public void ReplayTutorial()
    {
        OpenTutorialScene();
    }

    /// <summary>
    /// ฟังก์ชันส่วนตัว: เปิด Tutorial Scene และแสดง step แรก
    /// </summary>
    private void OpenTutorialScene()
    {
        currentTutorialStep = 0;

        if (tutorialScene != null)
            tutorialScene.SetActive(true);

        ShowTutorialStep(0);
    }

    /// <summary>
    /// ปุ่ม Next จะเรียกฟังก์ชันนี้
    /// ถ้าเลยหน้าสุดท้าย จะปิด Tutorial Scene
    /// </summary>
    public void GoToNextTutorialStep()
    {
        currentTutorialStep++;

        if (currentTutorialStep >= tutorialSteps.Length)
        {
            // ครบทุกหน้า ปิด Tutorial Scene
            CloseTutorialScene();
            return;
        }

        ShowTutorialStep(currentTutorialStep);
    }

    /// <summary>
    /// แสดง step ที่กำหนด
    /// </summary>
    private void ShowTutorialStep(int step)
    {
        // ซ่อนทั้งหมด
        foreach (var tutorialStep in tutorialSteps)
        {
            if (tutorialStep != null)
                tutorialStep.SetActive(false);
        }

        // แสดงเฉพาะที่กำหนด
        if (step >= 0 && step < tutorialSteps.Length && tutorialSteps[step] != null)
        {
            tutorialSteps[step].SetActive(true);
        }
    }

    /// <summary>
    /// ปิด Tutorial Scene
    /// </summary>
    private void CloseTutorialScene()
    {
        if (tutorialScene != null)
            tutorialScene.SetActive(false);

        currentTutorialStep = 0;
    }
}