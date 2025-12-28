using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WordStore : MonoBehaviour
{
   [SerializeField] private List<WordData> allWords;

   public Button NextWord;
    public List<WordData> Hard;
   
    public List<WordData> Medium;
    public List<WordData> Easy;

    public List<WordData> WordSetData;


    public List<WordData> remainingWords;

    public TextMeshProUGUI EnglishText;
    public TextMeshProUGUI ThaiText;

    public int a = 0;


    public void Awake()
    {
        ResetPool();

        Setdifficulty();

        WordSetting();
    }

    public void Start()
    {
        NextWord.onClick.AddListener(OnmyButtonclick);
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

        WordData word;
        if (remainingWords.Count == 0)
            ResetPool();

        

        if (Easy.Count > 0)
        {
            int Index = Random.Range(0, Easy.Count);
            word = Easy[Index];
            Easy.RemoveAt(Index);
            remainingWords.Remove(word);

            return word;

        }
        else if (Medium.Count > 0 && Easy.Count == 0)
        {
            int Index = Random.Range(0, Medium.Count);
            word = Medium[Index];
            Medium.RemoveAt(Index);
            remainingWords.Remove(word);

            return word;

        }
        else if (Hard.Count > 0 && Medium.Count == 0 && Easy.Count == 0)
        {
            
            int Index = Random.Range(0, Hard.Count);
            word = Hard[Index];
            Hard.RemoveAt(Index);
            remainingWords.Remove(word);

            return word;

        }

        
       
        //int index = Random.Range(0, remainingWords.Count);
        //word = remainingWords[index];

        
        return null;

    
        //Debug.Log("Get Random Word Activated");

        //remainingWords.RemoveAt(index);
        //return word;

        

        
        
    }

    
    public void OnmyButtonclick()
    {
        WordData word = WordSetData[a];
        a++;
        ThaiText.text = word.thai;
        EnglishText.text = word.english;
        if (a == WordSetData.Count)
        {
            a = 0;

        }

        
    }
    /*public void wordintroduction(List<WordData> wordDataset)
    {
       
        WordData word = wordDataset[a];
        a++;
        ThaiText.text = word.thai;
        EnglishText.text = word.english;
        if (a == wordDataset.Count)
        {
            a = 0;

        }
    }*/

    public WordData WordSetting()
    {

        WordData word;

        int i = 0;

        while (i < 5)
        {
            
            word = GetRandomWord();
            i = i +1;
            WordSetData.Add(word);

            Debug.Log(i);

            
        }
        //wordintroduction(WordSetData);
        OnmyButtonclick();

        

        return null;
    }
}
