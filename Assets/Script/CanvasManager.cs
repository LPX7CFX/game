using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject settingsCanvas;
    private GameObject lastCanvas;

    public void OpenSettings(GameObject currentCanvas)
    {
        lastCanvas = currentCanvas;
        currentCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        lastCanvas.SetActive(true);
    }
}
