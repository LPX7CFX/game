using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using System.Linq;
using System;
using JetBrains.Annotations;
using UnityEditor.Callbacks;
using UnityEditor.PackageManager;

public class TypingManager : MonoBehaviour
{
    [SerializeField] RectTransform wordContainer;
    [SerializeField] GameObject letterPrefab;
    [SerializeField] private WordStore wordStore;
     
    [SerializeField] private TextMeshProUGUI thaiText;

    [SerializeField] GameObject StartUi;

    private List<Letterui> letters = new();
    private int currentIndex = 0;

    private int CountingSys = 1;
    private int wordSys;
    

    


    void Start()
    {
        
        wordStore.ResetPool();

        StartNextWord();

       
    }

    void Update()
    {


        if (currentIndex >= letters.Count)
        {
            StartNextWord();

        }
            

        if (!Input.anyKeyDown && CountingSys == wordSys )
            return;
            Debug.Log(letters.Count);
            Debug.Log("InputZone: " + currentIndex);
            Debug.Log("Pass The Check");
            

        string input = Input.inputString;
        
        

        if (string.IsNullOrEmpty(input))
            return;

        char typedChar = char.ToUpper(input[0]);

        Letterui currentLetter = letters[currentIndex];

        
        

        if (typedChar == currentLetter.Value && CountingSys == wordSys)
        {
            Debug.Log("The Typed Char Worked");
            currentLetter.MarkCorrect();
            currentIndex++;
        }
        else if(typedChar != currentLetter.Value && CountingSys == wordSys)
        {
            Debug.Log("Wrong Letter Check Work");
            // Optional: show wrong letter
            currentLetter.FlashWrong();
        }
        Debug.Log(currentIndex);
        Debug.Log(currentLetter.Value);
        /*if (currentIndex == letters.Count && letters[currentIndex] == letters[letters.Count])
        {
            StartNextWord();

        }*/


        
        /*if (Input.anyKeyDown) return;
        Debug.Log("UpdateFirst");

        
        string input = Input.inputString;
        Debug.Log("UpdateSecond");
        if (string.IsNullOrEmpty(input)) return;
        Debug.Log("UpdateThird");

        char typed = char.ToUpper(input[0]);
        Debug.Log("UpdateFourth");
        CheckLetter (typed);*/
        


        
    
    }

    IEnumerator CreateWord(string word)
    {

        Debug.Log("Start");
        



        foreach (char C in word)
        {
            Debug.Log("Spawn letter: " + C);

            GameObject go = Instantiate(letterPrefab);

            Debug.Log("Instantiated");

            go.transform.SetParent(wordContainer,false);

            var letter = go.GetComponent<Letterui>();
            letter.setletter(C);
            letters.Add(letter);
            

            CountingSys++;
            Debug.Log(CountingSys);
            Debug.Log(letters.Count);
            Debug.Log(wordSys);

            yield  return new WaitForSeconds(0.5f);
        }


    }

    void CheckLetter(char typed)
    {
        if (currentIndex >= letters.Count) return;
        Debug.Log("CheckLetterFirst");

        var letter = letters[currentIndex];
        Debug.Log("CheckLetterSecond");
        if (letter.Value == typed)
        {
            Debug.Log("CheckLetterThird");
            letter.MarkCorrect();
            Debug.Log("CheckLetterFourth");
            currentIndex++;
            Debug.Log("CheckLetterFifth");
        }
        else
        {
            Debug.Log("CheckLetterSixth");
            letter.MarkWrong();
            Debug.Log("CheckLetterSeventh");
        }
    }


    public void StartNextWord()
    {
        ClearCurrentWord();

        WordData wordData = wordStore.GetRandomWord();
        wordSys = wordData.english.Length;
        thaiText.text = wordData.thai;
        StartCoroutine(CreateWord(wordData.english));
        Debug.Log("StartNextWordActivated");

        currentIndex = 0;
        

    }
    void ClearCurrentWord()
    {
        CountingSys = 0;
        foreach (Transform child in wordContainer)
        {
            Destroy(child.gameObject);
        }

        letters.Clear();
    }
    /*void CallCreate()
    {
        StartCoroutine(CreateWord(WordData,english));

    }*/


}


    /*void CheckInput(char typed)
{
    if (currentIndex >= letters.Count)
        return;

    char expectedChar = letters[currentIndex].GetLetter();

    if (char.ToUpper(typed) == char.ToUpper(expectedChar))
    {
        letters[currentIndex].MarkCorrect();
        currentIndex++;
    }
    else
    {
        letters[currentIndex].MarkWrong();
    }
}*/

