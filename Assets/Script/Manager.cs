using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject Code;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Canvas.activeSelf == false)
        {
            Code.SetActive(false);

        }
        else
        {
            
            Code.SetActive(true);
        }
        
    }
}
