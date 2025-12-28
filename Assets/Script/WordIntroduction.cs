using UnityEngine;
using System.Collections.Generic;

public class WordIntroduction : MonoBehaviour
{
    [SerializeField] WordStore wordStore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wordStore.WordSetting();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
