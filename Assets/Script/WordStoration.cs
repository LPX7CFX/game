using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WordStore : MonoBehaviour
{
   [SerializeField] private List<WordData> allWords;
    public List<WordData> Hard;
   
    public List<WordData> Medium;
    public List<WordData> Easy;


    public List<WordData> remainingWords;


    public void Awake()
    {
        ResetPool();

        Setdifficulty();
    }

    public void ResetPool()
    {
        remainingWords = new List<WordData>(allWords);
    }

    public void Setdifficulty()
    {
        int WordCount = allWords.Count;

        int WordIndex = WordCount--;


        

        foreach(WordData word in allWords)
        {
            int LettersCount = word.english.Length;

            if(LettersCount >= 0 && LettersCount <= 5)
            {
                Easy.Add(word);

            }
            else if(LettersCount >= 6 && LettersCount <= 8)
            {
                Medium.Add(word);

            }
            else
            {
                Hard.Add(word);
            }

        }

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
