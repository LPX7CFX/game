using UnityEngine;

public class ExitGameButton : MonoBehaviour

{
    public void QuitGame()
    {
        Debug.Log("กำลังออกจากเกม... (Quitting Game)");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}