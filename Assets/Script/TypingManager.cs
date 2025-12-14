using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TypingManager : MonoBehaviour
{
    [SerializeField] RectTransform wordContainer;
    [SerializeField] GameObject letterPrefab;

    private List<Letterui> letters = new();
    private int currentIndex = 0;

    void Start()
    {
        
        /*CreateWord("UNITY");*/
        StartCoroutine(CreateWord("UNITY"));
       
    }

    void Update()
    {
        if (Input.anyKeyDown) return;
        Debug.Log("UpdateFirst");

        
        string input = Input.inputString;
        Debug.Log("UpdateSecond");
        if (string.IsNullOrEmpty(input)) return;
        Debug.Log("UpdateThird");

        char typed = char.ToUpper(input[0]);
        Debug.Log("UpdateFourth");
        CheckLetter (typed);

        
    
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

            yield  return new WaitForSeconds(2f);
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
}
