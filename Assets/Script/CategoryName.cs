using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
public class CategoryName : MonoBehaviour
{

    public TMP_InputField categoryinput;
    public Button Enter;
    public RectTransform CategoryZone; 
    public GameObject Category;
    private TextMeshProUGUI CategoryNameText;
    private GameObject GameButton;
    public Button Exit;
    public Button EnterGUI;
    public Button EnterVocabll;
    public GameObject CateAddGUI;
    public GameObject WordAddGUI;
    [SerializeField] private WordData worddata;
    [SerializeField] private WordStore wordstore;
    //public List<Cate> catee = new();
    public List<Cate> cate = new();
    public Cate Cates;
    public TMP_InputField English;
    public TMP_InputField Thai;
    public RectTransform TheHolder;
    public GameObject Word;
    
    public string IdSent;
    
    
    
    
    private HashSet<string> HasSaved = new HashSet<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Enter.onClick.AddListener(NameEnter);
        Exit.onClick.AddListener(Close);
        EnterVocabll.onClick.AddListener(EnterVocab);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void NameEnter()
    {
        string categoryname = categoryinput.text.Trim();
        
        if(HasSaved.Contains(categoryname)) return; 

        if(string.IsNullOrEmpty(categoryname)) return;
 
        GameObject CategoryBlocks = Instantiate(Category, CategoryZone, false);
        CategoryBlocks.transform.SetSiblingIndex(0);
        GameButton = CategoryBlocks;
        var category = CategoryBlocks.GetComponent<ClassForCategory>();
        category.ID = categoryname;

        Button CategorySuperButton = CategoryBlocks.GetComponent<Button>();
        CategorySuperButton.onClick.AddListener(() => Print(category.ID)) ;
        HasSaved.Add(categoryname);

        

        Transform CategoryRealBlocks = CategoryBlocks.transform.GetChild(0);

        //Transform CategoryNameTextTrans = CategoryRealBlocks.GetChild(0);
        CategoryNameText = CategoryRealBlocks.GetComponent<TextMeshProUGUI>();

        CategoryNameText.text  = categoryname;
        cate.Add(new Cate { Name = categoryname});





    }
    void Print(string ID)
    {
        TextMeshProUGUI Word = GameButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        



        Debug.Log(ID);
        IdSent = ID;
        CreateWordthreethousand();




    }
    void Close()
    {
        CateAddGUI.SetActive(false);
        WordAddGUI.SetActive(true);



    }
    void createnewlistforcate(string Topic)
    {

        if(cate.Find(c => c.Name == Topic) != null) return;

        Cate newcate = new Cate();
        newcate.Name = Topic;
        newcate.words = new List<WordData>();


        cate.Add(newcate);
        
        

       



    }
    void EnterVocab()
    {
        string EnglishVocab = English.text.Trim();
        string ThaiVocab = Thai.text.Trim();
        if(string.IsNullOrEmpty(IdSent)) return;

        
        Cate category = cate.Find(c => c.Name == IdSent);
        Debug.Log(category);

        if(string.IsNullOrEmpty(EnglishVocab)||string.IsNullOrEmpty(ThaiVocab)) return;
        if (category != null)
        {
            bool exist = category.words
                .Any(w => w.english.Equals(EnglishVocab, StringComparison.OrdinalIgnoreCase));
            if (exist)
        {
            
            return;
        }
        }

        
        category.words.Add(new WordData { english = EnglishVocab});
        category.words.Add(new WordData { thai = ThaiVocab});
        CreateWordthreethousand();
        //GameObject WordExtra = Instantiate(Word, TheHolder, false);
        //GameObject InsideHolder = WordExtra.GetChild[1];
        //TextMeshProUGUI Vocab = InsideHolder.GetChild[0].GetComponent<TextMeshProUGUI>();
        //Vocab.text = 


       

        






    }
    void CreateWordthreethousand()
    {
        Debug.Log(IdSent);
        if (cate.Count == 0) return;
        Cate category = cate.Find(c => c.Name == IdSent);
        int holdercount = TheHolder.transform.childCount;
        Transform Inside = Word.transform.GetChild(1);
        TextMeshProUGUI En = Inside.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI Th = Inside.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
        int i = 0;





        foreach (WordData words in category.words)
        {
            if(holdercount == category.words.Count) break;
            Debug.Log("BUggg");
            if(category.words.Count == 0)break;
            Debug.Log("BuGGG");

            if(holdercount == 0)
            {
                GameObject Safe = Instantiate(Word, TheHolder, false);
                Debug.Log("bUGGGG");
                
                Transform ETT = Safe.transform.GetChild(1);
                TextMeshProUGUI Eng = ETT.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI Tha = ETT.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
                string interesteden = category.words[i].english;
                i++;
                Eng.text = interesteden;


            }
            else if(category.words.Find(c => c.english == En.text) == null&&holdercount!=0){
            
                GameObject Safe = Instantiate(Word, TheHolder, false);
                Debug.Log("bUGGGG");
                
                Transform ETT = Safe.transform.GetChild(1);
                TextMeshProUGUI Eng = ETT.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI Tha = ETT.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
                string interesteden = category.words[i].english;
                i++;
                Eng.text = interesteden;
                

            }
            else break;



        }
        i = 0;

        


    }
}
