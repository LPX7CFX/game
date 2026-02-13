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
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button openTutorialButton;

    private int currentPage = 0;

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

        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    private void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(GoToNextPage);

        if (previousButton != null)
            previousButton.onClick.AddListener(GoToPreviousPage);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseTutorial);

        if (openTutorialButton != null)
            openTutorialButton.onClick.AddListener(OpenTutorial);
    }

    public void OpenTutorial()
    {
        currentPage = 0;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);

        ShowPage(0);
        UpdateButtonStates();
    }

    public void GoToNextPage()
    {
        currentPage++;

        if (currentPage >= tutorialPages.Length)
        {
            CloseTutorial();
            return;
        }

        ShowPage(currentPage);
        UpdateButtonStates();
    }

    public void GoToPreviousPage()
    {
        currentPage--;

        if (currentPage < 0)
        {
            currentPage = 0;
            return;
        }

        ShowPage(currentPage);
        UpdateButtonStates();
    }

    private void ShowPage(int currentPage)
    {
        foreach (var page in tutorialPages)
        {
            if (page != null)
                page.SetActive(false);
        }

        if (currentPage >= 0 && currentPage < tutorialPages.Length && tutorialPages[currentPage] != null)
        {
            tutorialPages[currentPage].SetActive(true);
        }
    }

    private void UpdateButtonStates()
    {
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(currentPage > 0);
        }

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(currentPage < tutorialPages.Length - 1);
        }
    }
    public void CloseTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        currentPage = 0;
    }
}