using UnityEngine;

public class UiSystem : MonoBehaviour
{
    public static UiSystem Instance; // สร้างตัวแปรให้เรียกใช้จากที่อื่นง่ายๆ

    [Header("All Panels (ลาก Panel มาใส่ตรงนี้)")]
    [SerializeField] GameObject startUiPanel;   // หน้าเมนูหลัก
    [SerializeField] GameObject settingsPanel;  // หน้าตั้งค่า
    [SerializeField] GameObject leaderboardPanel; // หน้าตารางคะแนน
    [SerializeField] GameObject decoratePanel;  // หน้าตกแต่ง
    [SerializeField] GameObject gamePlayPanel;  // หน้าตอนเล่นเกม (Health bar, Score)

    private void Awake()
    {
        // Setup Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // เริ่มเกมมา บังคับเปิดหน้า StartUI และปิดหน้าอื่นให้หมด
        OpenMainData();
    }

    // ฟังก์ชันสำหรับสลับหน้า (ใช้หลักการ ปิดทั้งหมดก่อน แล้วเปิดตัวที่ต้องการ)
    private void CloseAllPanels()
    {
        if (startUiPanel) startUiPanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (leaderboardPanel) leaderboardPanel.SetActive(false);
        if (decoratePanel) decoratePanel.SetActive(false);
        if (gamePlayPanel) gamePlayPanel.SetActive(false);
    }

    // --- ส่วนของปุ่มกด (เอาไปผูกกับ Button) ---

    public void OpenMainData() // กลับหน้าเมนู
    {
        CloseAllPanels();
        if (startUiPanel) startUiPanel.SetActive(true);
    }

    public void OpenSettings() // เปิด Setting
    {
        CloseAllPanels();
        if (settingsPanel) settingsPanel.SetActive(true);
    }

    public void OpenLeaderboard() // เปิด Leaderboard
    {
        CloseAllPanels();
        if (leaderboardPanel) leaderboardPanel.SetActive(true);
    }

    public void OpenDecorate() // เปิด Decorate
    {
        CloseAllPanels();
        if (decoratePanel) decoratePanel.SetActive(true);
    }

    // --- ฟังก์ชันเริ่มเกม (แก้ปัญหา Code run before canvas open) ---
    public void StartGameMatch()
    {
        CloseAllPanels();
        if (gamePlayPanel) gamePlayPanel.SetActive(true);

        // ตรงนี้คือจุดสำคัญ! สั่งให้เกมเริ่มทำงานจริงๆ ตรงนี้
        Debug.Log("Game Started! Logic begins now.");

        // Example: ถ้ามี Script คุมเกมชื่อ GameManager
        // GameManager.Instance.StartLevel(); 
    }
}