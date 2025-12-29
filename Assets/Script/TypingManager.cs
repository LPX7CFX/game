using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using System.Linq;
using System;
using JetBrains.Annotations;
using UnityEditor.Callbacks;
using UnityEditor.PackageManager;
using UnityEngine.UI;

public class TypingManager : MonoBehaviour
{
    [SerializeField] RectTransform wordContainer;
    [SerializeField] GameObject letterPrefab;
    [SerializeField] private WordStore wordStore;
     
    [SerializeField] private TextMeshProUGUI thaiText;

    [SerializeField] GameObject StartUi;

    [SerializeField] Button Training;

    [SerializeField] Button Challenge;
    [SerializeField] GameObject IntroScene;
    [SerializeField] GameObject gamescene;

    private List<Letterui> letters = new();
    private int currentIndex = 0;

    private int CountingSys = 1;

    private int startnextwordcontroller = 0;
    private int wordSys;
    private int startnextwordcontrollertrain = 0;
    private int indicator = 0;
    private int indicator2 = -1;
    

    


    void Start()
    {
        
        wordStore.ResetPool();

        Challenge.onClick.AddListener(StartNextWord);

        Training.onClick.AddListener(training);

       
    }

    void Update()
    {


        if (currentIndex >= letters.Count&&startnextwordcontroller != 0)
        {
            StartNextWord();

        }
        else if (currentIndex >= letters.Count&&startnextwordcontrollertrain != 0&&IntroScene.activeSelf == false&&startnextwordcontrollertrain<5)
        {
            training();

        }
        if(currentIndex >= letters.Count && startnextwordcontrollertrain == 5)
        {
            
            ClearCurrentWord();
        }
        if(indicator2 == 5&&indicator==0)
        {
            wordStore.WordSetting();
            indicator++;
            

        }

        if (indicator2 == 5&&wordStore.countingsht==2)
        {
            Debug.Log("Data:"+wordStore.WordSetData.Count);
            gamescene.SetActive(false);
            IntroScene.SetActive(true);
            startnextwordcontrollertrain = 0;
            indicator=0;
            indicator2=-1;


        }
        else
        {
            Debug.Log("lose");
            Debug.Log("Lose::"+startnextwordcontrollertrain);
            Debug.Log("Indicator2:"+indicator2);
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
            Debug.Log("DataWord:"+ wordStore.WordSetData.Count);

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

        startnextwordcontroller++;
        

    }
    void ClearCurrentWord()
    {
        CountingSys = 0;
        foreach (Transform child in wordContainer)
        {
            Destroy(child.gameObject);
        }

        letters.Clear();
        startnextwordcontrollertrain++;
        indicator2++;

    }

    void training()
    {

        ClearCurrentWord();
        
            

        WordData wordDatas = wordStore.getrandomwordtraining();
        wordSys = wordDatas.english.Length;
        thaiText.text = wordDatas.thai;
        StartCoroutine(CreateWord(wordDatas.english));
        Debug.Log("StartNextWordActivated");
        
        

        currentIndex = 0;
        if (IntroScene.activeSelf == true)
        {
            IntroScene.SetActive(false);
            gamescene.SetActive(true);

        }
        else if (IntroScene.activeSelf == false&&gamescene.activeSelf == false)
        {
            gamescene.SetActive(true);

        }
            

        
        
        
        
        

        
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

