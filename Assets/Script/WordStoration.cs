using System.Collections.Generic;
using UnityEngine;

public class WordStore : MonoBehaviour
{
   [SerializeField] private List<WordData> allWords;

    public List<WordData> remainingWords;

    public void Awake()
    {
        ResetPool();
    }

    public void ResetPool()
    {
        remainingWords = new List<WordData>(allWords);
    }

    public WordData GetRandomWord()
    {
        if (remainingWords.Count == 0)
            ResetPool();

        int index = Random.Range(0, remainingWords.Count);
        WordData word = remainingWords[index];
        Debug.Log("Get Random Word Activated");

        remainingWords.RemoveAt(index);
        return word;
        
    }
}
