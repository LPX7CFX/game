using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Letterui : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI letterText;
    [SerializeField] Image background;
    public char Value { get; private set;}
    public bool Typed {get; private set;}

    public void setletter(char C)
    {
         Debug.Log(
        "SetLetter called on " + gameObject.name +
        " with " + C +
        ", text ref = " + letterText
    );

         letterText.text = C.ToString();

        /*Value = C;
        letterText.text = C.ToString();*/

    }

     public char GetLetter()
    {
        return Value;
    }

    public void MarkCorrect()
    {
        background.color = Color.green;
        Typed = true;

    
    }

    public void MarkWrong()
    {
        background.color = Color.red;


    }



}
