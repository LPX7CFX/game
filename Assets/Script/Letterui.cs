using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Collections;

public class Letterui : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI letterText;
    [SerializeField] Image background;
    public char Value { get; private set;}
    public bool Typed {get; private set;}


    void Awake()
    {
          if (letterText == null)
            letterText = GetComponentInChildren<TextMeshProUGUI>();

        
        letterText.text = "";
    }

    public void setletter(char C)
    {
        /* Debug.Log(
        "SetLetter called on " + gameObject.name +
        " with " + C +
        ", text ref = " + letterText
    );

         letterText.text = C.ToString();*/

        Value = C;
        
        /*letterText.text = C.ToString();*/

    }

     public char GetLetter()
    {
        return Value;
    }

    public void MarkCorrect()
    {
        letterText.text = Value.ToString();
        Debug.Log(letterText.text);
        background.color = Color.white;

        

    
    }

    public void FlashWrong()
    {
        StopAllCoroutines();
        StartCoroutine(MarkWrong());

    }

    public IEnumerator MarkWrong()
    {
        background.color = Color.red;
        
        yield return new WaitForSeconds(1f);

        background.color = Color.white;


    }



}
