using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WordStore : MonoBehaviour
{
   [SerializeField] public List<WordData> allWords;

   public Button NextWord;
   public Button Training;
   public Button Exit;
   public Button ExitTraining;
    public List<WordData> Hard;
   
    public List<WordData> Medium;
    public List<WordData> Easy;

    public List<WordData> WordSetData;


    public List<WordData> remainingWords;
    public List<WordData> EasyWord;
    public List<WordData> MediumWord;
    public List<WordData> HardWord;

    public TextMeshProUGUI EnglishText;
    public TextMeshProUGUI ThaiText;

    public int a = 0;
    public int countingsht = 0;
    public int countingsht2 = 0;
    public int countingsht3 = 0;
    public int countingsht4 = 0;
    public int countingsht5 = 0;
    public int countingsht6 = 0;


    public void Awake()
    {
        ResetPool();

        Setdifficulty();
        

        
    }

    public void Start()
    {
        ExitTraining.onClick.AddListener(ResetWordDiffTraining);
        Exit.onClick.AddListener(ResetWordDiff);
        Training.onClick.AddListener(training);
        NextWord.onClick.AddListener(OnmyButtonclick);
        countingsht5 = allWords.Count;
        Debug.Log("ClearlyAsday"+countingsht5);
       
       

    }
    public void Update()
    {
         if(remainingWords.Count == 0)
        {
            ResetPool();

        }
    }


    public void ResetPool()
    {
        remainingWords = new List<WordData>(allWords);
        countingsht2 = 0;
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
                EasyWord.Add(word);

            }
            else if(LettersCount >= 6 && LettersCount <= 8)
            {
                Medium.Add(word);
                MediumWord.Add(word);

            }
            else
            {
                Hard.Add(word);
                HardWord.Add(word);
            }

        }

    }
    public void ResetWordDiff()
    {
        Easy = new List<WordData>(EasyWord);
        Medium = new List<WordData>(MediumWord);
        Hard = new List<WordData>(HardWord);


    }
    public void ResetWordDiffTraining()
    {   Easy = new List<WordData>(EasyWord);
        Medium = new List<WordData>(MediumWord);
        Hard = new List<WordData>(HardWord);
        WordSetData.Clear();
        

    }



    public WordData GetRandomWord()
    {
        
        WordData word;
        if (remainingWords.Count == 0)
            
            ResetPool();
            countingsht2++;
            countingsht6++;

        

        if (Easy.Count > 0)
        {
            int Index = Random.Range(0, Easy.Count);
            word = Easy[Index];
            Easy.RemoveAt(Index);
            

            return word;

        }
        else if (Medium.Count > 0 && Easy.Count == 0)
        {
            int Index = Random.Range(0, Medium.Count);
            word = Medium[Index];
            Medium.RemoveAt(Index);
            

            return word;

        }
        else if (Hard.Count > 0 && Medium.Count == 0 && Easy.Count == 0)
        {
            
            int Index = Random.Range(0, Hard.Count);
            word = Hard[Index];
            Hard.RemoveAt(Index);
            

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

        NextWord1();
        

        
        

        
    }
    public void NextWord1()
{
    if (WordSetData == null || WordSetData.Count == 0) return;

    if (a < 0 || a >= WordSetData.Count)
        a = 0;

    WordData word = WordSetData[a];

    ThaiText.text = word.thai;
    EnglishText.text = word.english;

    a++;
}

    public WordData getrandomwordtraining()
    {
        WordData words;

        
        int IndexWordrandom = Random.Range(0, WordSetData.Count);
        words = WordSetData[IndexWordrandom];
        WordSetData.RemoveAt(IndexWordrandom);
        remainingWords.Remove(words);
        


        return words;
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

        int i;
        i = 0;

        while (i < 5)
        {
            
            word = GetRandomWord();
            i = i +1;
            if (word == null)
                continue;

            if (string.IsNullOrEmpty(word.english))
                continue;
            WordSetData.Add(word);
            Debug.Log("word:"+i);

            

            
        }
        //wordintroduction(WordSetData);
        countingsht++;
        countingsht3 = WordSetData.Count;
        
        if (countingsht3 < 5)
        {
            countingsht4++;

        }
        else
        {
            countingsht4=0;
        }
        Debug.Log("ClearAsdayCount4"+countingsht4);
    
        NextWord1();

        if (countingsht > 2)
        {
            
            countingsht = 2;
            
        }
       

        
        

        return null;
    }
    public void training()
    {
        
        WordSetting();
    }
}
