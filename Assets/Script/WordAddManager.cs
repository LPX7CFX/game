using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.Common;


public class WordAddManager : MonoBehaviour
{
    //this code is about control the word add system which is like leaderboard + namemanager but a lot harder
//-------------------------------------------------------------------------------------------------------------------

    [SerializeField] private WordData WD;  //worddatavariable for the usage in class
    [SerializeField] private WordStore WS; //wordstorevariable for the usage in variable most of the variable to be exact
    [SerializeField] private wordsavehandeler WSH;//possibly wordSavehandeler still don't know
    public Button CategoryAdd; //buttoncategorycreate
    
    public Button WordAdd;//button for create word
    public Button Delete;//button for deletion
    public TMP_InputField VocabEnter; //text input for enter vocab
    public TMP_InputField TranslateEnter;//text input for enter trans

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*needed function
    1. for accept a vocab which can only be english for now traslation possibly everyting in one func
    
    
    
    
    
    */
    void AddCategory()
    {
        

    }
    void addword()
    {
        


    }
    void deleteword()
    {
        

    }
}
