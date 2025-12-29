using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject settingsCanvas;
    private GameObject lastCanvas;

    // เรียกตอนกดปุ่ม Setting
    public void OpenSettings(GameObject currentCanvas)
    {
        lastCanvas = currentCanvas;
        currentCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    // เรียกตอนกดปุ่ม Close / Back
    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        lastCanvas.SetActive(true);
    }
}
