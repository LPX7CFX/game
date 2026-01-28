using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class Categorychoose : MonoBehaviour
{

    [SerializeField] private WordStore ws;
    public GameObject Cateselect;
    public GameObject ModeScene;
    public RectTransform Catepanel;
    [SerializeField] private TypingManager tm;
    public HashSet<string> hasalready = new HashSet<string>();

    void Start()
    {
        cateseperater();
        starttype();
    }


    void Update()
    {

    }

    void cateseperater()
    {
        foreach (WordData word in ws.allWords)
        {
            if (hasalready.Contains(word.category))
            {
                continue;
            }
            hasalready.Add(word.category);
            GameObject cate3k = Instantiate(Cateselect, Catepanel, false);
            var compo = cate3k.GetComponent<classforcatesel>();
            compo.categoryname = word.category;
            TextMeshProUGUI textmesh = cate3k.GetComponentInChildren<TextMeshProUGUI>();
            Button catebutt = cate3k.GetComponent<Button>();
            catebutt.onClick.AddListener(() => sentsresult(compo.categoryname));
            textmesh.text = word.category;




        }
        foreach (WordData word in ws.allWords)
        {
            if (hasalready.Contains(word.verb))
            {
                continue;
            }
            hasalready.Add(word.verb);
            GameObject cate3k = Instantiate(Cateselect, Catepanel, false);
            var compo = cate3k.GetComponent<classforcatesel>();
            compo.categoryname = word.verb;
            TextMeshProUGUI textmesh = cate3k.GetComponentInChildren<TextMeshProUGUI>();
            Button catebutt = cate3k.GetComponent<Button>();
            catebutt.onClick.AddListener(() => sentsresultverb(compo.categoryname));
            textmesh.text = word.verb;

        }


    }
    void sentsresult(string categoryname)
    {
        List<WordData> selectedwords = ws.allWords.Where(word => word.category == categoryname).ToList();

        ws.remainingWords = selectedwords;
        ws.NewSetdifficulty();
        if (ws.T != 0)
        {
            ws.T = 0;
            ws.training();
            ModeScene.SetActive(false);

        }
        if (tm.C != 0)
        {
            tm.C = 0;
            tm.StartNextWord();
            ModeScene.SetActive(false);

        }


    }
    void sentsresultverb(string verbname)
    {
        List<WordData> selectedwords = ws.allWords.Where(word => word.verb == verbname).ToList();

        ws.remainingWords = selectedwords;
        ws.NewSetdifficulty();
        if (ws.T != 0)
        {
            ws.T = 0;
            ws.training();
            ModeScene.SetActive(false);

        }
        if (tm.C != 0)
        {
            tm.C = 0;
            tm.StartNextWord();
            ModeScene.SetActive(false);

        }
    }
    void starttype()
    {
        GameObject cate3k = Instantiate(Cateselect, Catepanel, false);
        var compo = cate3k.GetComponent<classforcatesel>();
        TextMeshProUGUI textmesh = cate3k.GetComponentInChildren<TextMeshProUGUI>();
        Button catebutt = cate3k.GetComponent<Button>();
        catebutt.onClick.AddListener(origin);
        textmesh.text = "AllWords";



    }
    void origin()
    {

        ws.remainingWords = ws.allWords;
        ws.NewSetdifficulty();
        if (ws.T != 0)
        {
            ws.T = 0;
            ModeScene.SetActive(false);
            ws.training();

        }
        if (tm.C != 0)
        {
            tm.C = 0;
            ModeScene.SetActive(false);
            tm.StartNextWord();

        }

    }
}