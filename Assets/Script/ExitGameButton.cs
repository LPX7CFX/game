using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    // ลากฟังก์ชันนี้ไปใส่ในปุ่ม
    public void QuitGame()
    {
        // 1. แจ้งเตือนใน Console ว่ากดแล้ว
        Debug.Log("กำลังออกจากเกม... (Quitting Game)");

        // 2. สั่งปิดโปรแกรม (สำหรับตัวเกมจริง)
        Application.Quit();

        // 3. สั่งหยุดเล่น (สำหรับเทสใน Unity Editor)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}