using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeTutorial : MonoBehaviour
{
    public static ModeTutorial Instance { get; private set; }

    [Header("Tutorial Scene")]
    [SerializeField] private GameObject tutorialScene;
    [SerializeField] private string modeSceneName = "Mode Scene";

    [Header("Tutorial Steps (Canvas/GameObjects)")]
    [SerializeField] private GameObject[] tutorialSteps = new GameObject[5];

    [Header("Control Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button closeButton; // ✅ เพิ่ม
    [SerializeField] private Button modeSceneReplayButton;

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

        if (tutorialScene != null)
            tutorialScene.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ✅ ระบบแสดง Tutorial ครั้งแรกตอนเข้า Mode Scene (เก็บไว้)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == modeSceneName && isFirstGameStart && !hasShownFirstTimeOnModeScene)
        {
            isFirstGameStart = false;
            ShowTutorialFirstTime();
        }
    }

    private void Start()
    {
        if (playButton != null)
            playButton.onClick.AddListener(ShowTutorialFirstTime);

        if (nextButton != null)
            nextButton.onClick.AddListener(GoToNextTutorialStep);

        if (previousButton != null)
            previousButton.onClick.AddListener(GoToPreviousTutorialStep);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseTutorialScene); // ✅ เพิ่ม

        if (modeSceneReplayButton != null)
            modeSceneReplayButton.onClick.AddListener(ReplayTutorial);
    }

    // ✅ แสดง Tutorial ครั้งแรก (เก็บไว้)
    public void ShowTutorialFirstTime()
    {
        if (!hasShownFirstTimeOnModeScene)
        {
            OpenTutorialScene();
            hasShownFirstTimeOnModeScene = true;
        }
    }

    // ✅ เปิด Tutorial ซ้ำได้ (เก็บไว้)
    public void ReplayTutorial()
    {
        OpenTutorialScene();
    }

    private void OpenTutorialScene()
    {
        currentTutorialStep = 0;

        if (tutorialScene != null)
            tutorialScene.SetActive(true);

        ShowTutorialStep(0);
        UpdateButtonStates(); // ✅ เพิ่ม - อัพเดทปุ่ม
    }

    public void GoToNextTutorialStep()
    {
        currentTutorialStep++;

        if (currentTutorialStep >= tutorialSteps.Length)
        {
            CloseTutorialScene();
            return;
        }

        ShowTutorialStep(currentTutorialStep);
        UpdateButtonStates(); // ✅ เพิ่ม
    }

    public void GoToPreviousTutorialStep()
    {
        currentTutorialStep--;

        if (currentTutorialStep < 0)
        {
            currentTutorialStep = 0;
            return;
        }

        ShowTutorialStep(currentTutorialStep);
        UpdateButtonStates(); // ✅ เพิ่ม
    }

    private void ShowTutorialStep(int step)
    {
        foreach (var tutorialStep in tutorialSteps)
        {
            if (tutorialStep != null)
                tutorialStep.SetActive(false);
        }

        if (step >= 0 && step < tutorialSteps.Length && tutorialSteps[step] != null)
        {
            tutorialSteps[step].SetActive(true);
        }
    }

    // ✅ ฟังก์ชันใหม่ - ซ่อน/แสดงปุ่มอัตโนมัติ
    private void UpdateButtonStates()
    {
        // ซ่อน Previous ถ้าอยู่หน้าแรก (0)
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(currentTutorialStep > 0);
        }

        // ซ่อน Next ถ้าอยู่หน้าสุดท้าย
        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(currentTutorialStep < tutorialSteps.Length - 1);
        }
    }

    private void CloseTutorialScene()
    {
        if (tutorialScene != null)
            tutorialScene.SetActive(false);
        currentTutorialStep = 0;
    }
}