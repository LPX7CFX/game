using UnityEngine;
using System.Collections.Generic;

public class TypingManager : MonoBehaviour
{
    [SerializeField] RectTransform wordContainer;
    [SerializeField] GameObject letterPrefab;

    private List<Letterui> letters = new();
    private int currentIndex = 0;

    void Start()
    {
        CreateWord("UNITY");
    }

    void Update()
    {
        if (!Input.anyKeyDown) return;

        string input = Input.inputString;
        if (string.IsNullOrEmpty(input)) return;

        char typed = char.ToUpper(input[0]);
        CheckLetter(typed);
    }

    void CreateWord(string word)
    {
        foreach (char C in word)
        {
            var go = Instantiate(letterPrefab, wordContainer, false);
            var letter = go.GetComponent<Letterui>();
            letter.setletter(C);
            letters.Add(letter);
        }
    }

    void CheckLetter(char typed)
    {
        if (currentIndex >= letters.Count) return;

        var letter = letters[currentIndex];

        if (letter.Value == typed)
        {
            letter.MarkCorrect();
            currentIndex++;
        }
        else
        {
            letter.MarkWrong();
        }
    }
}
