using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [Header("Tutorial Scene")]
    [SerializeField] private GameObject tutorialScene;
    [SerializeField] private string modeSceneName = "Mode Scene";

    [Header("Tutorial Steps (Canvas/GameObjects)")]
    [SerializeField] private GameObject[] tutorialSteps = new GameObject[5];

    [Header("Control Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton; // เพิ่มปุ่ม Previous (ถ้ามี)
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

        if (previousButton != null) // เพิ่ม listener ปุ่ม Previous (ถ้ามี)
            previousButton.onClick.AddListener(GoToPreviousTutorialStep);

        if (modeSceneReplayButton != null)
            modeSceneReplayButton.onClick.AddListener(ReplayTutorial);
    }

    public void ShowTutorialFirstTime()
    {
        if (!hasShownFirstTimeOnModeScene)
        {
            OpenTutorialScene();
            hasShownFirstTimeOnModeScene = true;
        }
    }

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
        UpdateButtonStates(); // **เพิ่มบรรทัดนี้** - สำคัญมาก!
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
        UpdateButtonStates(); // **เพิ่มบรรทัดนี้**
    }

    // เพิ่มฟังก์ชัน Previous (ถ้าต้องการ)
    public void GoToPreviousTutorialStep()
    {
        currentTutorialStep--;

        if (currentTutorialStep < 0)
        {
            currentTutorialStep = 0;
            return;
        }

        ShowTutorialStep(currentTutorialStep);
        UpdateButtonStates(); // **เพิ่มบรรทัดนี้**
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

    /// <summary>
    /// ฟังก์ชันใหม่: อัพเดทสถานะปุ่ม
    /// </summary>
    private void UpdateButtonStates()
    {
        // ซ่อนปุ่ม Previous เมื่ออยู่หน้าแรก
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(currentTutorialStep > 0);
        }

        // ซ่อนปุ่ม Next เมื่ออยู่หน้าสุดท้าย
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