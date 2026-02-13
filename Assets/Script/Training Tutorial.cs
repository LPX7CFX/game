using UnityEngine;
using UnityEngine.UI;

public class TrainingTutorial : MonoBehaviour
{
    public static TrainingTutorial Instance { get; private set; }

    [Header("Tutorial Panel")]
    [SerializeField] private GameObject tutorialPanel;

    [Header("Tutorial Pages")]
    [SerializeField] private GameObject[] tutorialPages = new GameObject[5];

    [Header("Control Buttons")]
    [SerializeField] private Button nextButton; // ปุ่มไปหน้าถัดไป
    [SerializeField] private Button previousButton; // ปุ่มย้อนกลับ (เพิ่มใหม่)
    [SerializeField] private Button closeButton; // ปุ่มปิด
    [SerializeField] private Button openTutorialButton; // ปุ่มเปิด Tutorial

    private int currentPage = 0;

    private void Awake()
    {
        // Singleton Pattern
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

        // ปิด Tutorial Panel ตั้งแต่เริ่มต้น
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    private void Start()
    {
        // ลงทะเบียนปุ่มต่างๆ
        if (nextButton != null)
            nextButton.onClick.AddListener(GoToNextPage);

        if (previousButton != null)
            previousButton.onClick.AddListener(GoToPreviousPage); // เพิ่มใหม่

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseTutorial);

        if (openTutorialButton != null)
            openTutorialButton.onClick.AddListener(OpenTutorial);
    }

    /// <summary>
    /// เปิด Tutorial
    /// </summary>
    public void OpenTutorial()
    {
        currentPage = 0;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);

        ShowPage(0);
        UpdateButtonStates(); // เพิ่มใหม่: อัพเดทสถานะปุ่ม
    }

    /// <summary>
    /// ไปหน้าถัดไป
    /// </summary>
    public void GoToNextPage()
    {
        currentPage++;

        // ถ้าเกินหน้าสุดท้าย ให้ปิด Tutorial
        if (currentPage >= tutorialPages.Length)
        {
            CloseTutorial();
            return;
        }

        ShowPage(currentPage);
        UpdateButtonStates(); // เพิ่มใหม่: อัพเดทสถานะปุ่ม
    }

    /// <summary>
    /// ย้อนกลับหน้าก่อนหน้า (ฟังก์ชันใหม่)
    /// </summary>
    public void GoToPreviousPage()
    {
        currentPage--;

        // ถ้าน้อยกว่า 0 ให้อยู่หน้าแรก
        if (currentPage < 0)
        {
            currentPage = 0;
            return;
        }

        ShowPage(currentPage);
        UpdateButtonStates(); // อัพเดทสถานะปุ่ม
    }

    /// <summary>
    /// แสดงหน้าที่กำหนด
    /// </summary>
    private void ShowPage(int pageIndex)
    {
        // ซ่อนทุกหน้า
        foreach (var page in tutorialPages)
        {
            if (page != null)
                page.SetActive(false);
        }

        // แสดงเฉพาะหน้าที่ต้องการ
        if (pageIndex >= 0 && pageIndex < tutorialPages.Length && tutorialPages[pageIndex] != null)
        {
            tutorialPages[pageIndex].SetActive(true);
        }
    }

    /// <summary>
    /// อัพเดทสถานะปุ่ม Next และ Previous (ฟังก์ชันใหม่)
    /// ซ่อน/แสดงปุ่มตามหน้าที่อยู่
    /// </summary>
    private void UpdateButtonStates()
    {
        // ถ้าอยู่หน้าแรก (0) ให้ซ่อนปุ่ม Previous
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(currentPage > 0);
        }

        // เพิ่มใหม่: ถ้าอยู่หน้าสุดท้าย ให้ซ่อนปุ่ม Next
        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(currentPage < tutorialPages.Length - 1);
        }
    }

    /// <summary>
    /// ปิด Tutorial
    /// </summary>
    public void CloseTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        currentPage = 0;
    }
}