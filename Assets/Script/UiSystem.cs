using Unity.VisualScripting;
using UnityEngine;

public class UiSystem : MonoBehaviour
{
    [SerializeField] GameObject StartUi;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenGameUi()
    {
        StartUi.SetActive(false);
    }
}
