using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [Header("Tutorial UI")]
    [SerializeField] private GameObject tutorialPanel;

    [Header("Buttons")]
    [SerializeField] private Button startSceneTriggerButton; // ปุ่ม Play ใน Start Scene
    [SerializeField] private Button modeSceneButton;         // ปุ่มดู tutorial อีกครั้งใน Mode Scene
    [SerializeField] private Button closeTutorialButton;     // ปุ่ม Close ในหน้าต่าง tutorial

    // เก็บว่าโชว์แล้วครั้งหนึ่งในรอบการรันเกม
    private static bool hasShownThisRun = false;

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

        // ปิด panel ตั้งแต่เริ่มต้น
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    private void Start()
    {
        // ลงทะเบียน listener สำหรับปุ่มต่าง ๆ
        if (startSceneTriggerButton != null)
            startSceneTriggerButton.onClick.AddListener(ShowTutorialOnce);

        if (modeSceneButton != null)
            modeSceneButton.onClick.AddListener(ShowTutorialAlways);

        if (closeTutorialButton != null)
            closeTutorialButton.onClick.AddListener(HideTutorial);
    }

    // เรียกจากปุ่ม Play ใน Start Scene — โชว์เฉพาะครั้งแรก
    public void ShowTutorialOnce()
    {
        if (!hasShownThisRun)
        {
            ShowTutorial();
            hasShownThisRun = true;
        }
    }

    // เรียกจากปุ่มใน Mode Scene — โชว์ได้หลายครั้ง
    public void ShowTutorialAlways()
    {
        ShowTutorial();
    }

    // เปิด panel
    public void ShowTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
    }

    // ปิด panel
    public void HideTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }
}