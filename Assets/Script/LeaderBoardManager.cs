using System.Collections.Generic;
using System.Linq;

using UnityEngine.UI;
using UnityEngine;
using TMPro;





public class LeaderboardManager : MonoBehaviour
{
    public Transform contentParent;   // Where rows will be spawned
    public GameObject rowPrefab;      // One leaderboard row prefab
    public Categorychoose cc;
    public int maxShown = 10;
    public RectTransform Catebuttleadpanel;
    public GameObject Catebuttlead;
    [SerializeField] private WordStore ws;
    private HashSet<string> HasName = new HashSet<string>();


    public void Awake()
    {

    }
    public void Start()
    {
        buttins();
        buttverb();
        buttinsall();
    }
    public void buttins()
    {
        List<string> catename = ws.allWords.Select(w => w.category).Distinct().ToList();
        Debug.Log("CateName:" + catename.Count);
        Debug.Log("CateName:" + catename[0] + "," + catename[1] + "," + catename[2]);

        foreach (string word in catename)
        {
            GameObject catebuttleadpanel = Instantiate(Catebuttlead, Catebuttleadpanel, false);
            var cateleadbutt = catebuttleadpanel.transform.GetComponent<cateleadbutt>();
            cateleadbutt.UniqueID = word;
            var button = catebuttleadpanel.transform.GetComponent<Button>();
            button.GetComponentInChildren<TMP_Text>().text = word;
            button.onClick.AddListener(() => ShowLeaderboard(cateleadbutt.UniqueID));

        }



    }
    public void buttinsall()
    {
        GameObject catebuttleadpanel = Instantiate(Catebuttlead, Catebuttleadpanel, false);
        var cateleadbutt = catebuttleadpanel.transform.GetComponent<cateleadbutt>();
        cateleadbutt.UniqueID = "allword";
        var button = catebuttleadpanel.transform.GetComponent<Button>();
        button.GetComponentInChildren<TMP_Text>().text = "allword";
        button.onClick.AddListener(() => ShowLeaderboard(cateleadbutt.UniqueID));

    }
    public void buttverb()
    {
        List<string> catename = ws.allWords.Select(w => w.verb).Distinct().ToList();
        Debug.Log("CateName:" + catename.Count);
        Debug.Log("CateName:" + catename[0] + "," + catename[1] + "," + catename[2]);

        foreach (string word in catename)
        {
            GameObject catebuttleadpanel = Instantiate(Catebuttlead, Catebuttleadpanel, false);
            var cateleadbutt = catebuttleadpanel.transform.GetComponent<cateleadbutt>();
            cateleadbutt.UniqueID = word;
            var button = catebuttleadpanel.transform.GetComponent<Button>();
            button.GetComponentInChildren<TMP_Text>().text = word;
            button.onClick.AddListener(() => ShowLeaderboard(cateleadbutt.UniqueID));

        }


    }

    public void ShowLeaderboard(string type)
    {
        Debug.Log("ShowleaderboardStart");
        if (SaveManager.Instance == null) return;
        Debug.Log("ShowleaderBoardReturn");
        if (SaveManager.Instance.data == null) return;
        Debug.Log("ShowleaderBoardReturnData");

        // Clear old rows
        for (int i = contentParent.childCount - 1; i >= 0; i--)
        {
            Destroy(contentParent.GetChild(i).gameObject);
        }

        // Sort by LOWEST time (best first)
        /*List<LeaderboardEntry> sorted =
            SaveManager.Instance.data.entries
            .OrderBy(e => e.bestTimeSeconds)
            .Take(maxShown)
            .ToList();*/

        List<LeaderboardEntry> sorted2 = SaveManager.Instance.data.entries.Where(c => c.type == type).ToList();
        List<LeaderboardEntry> sorted3 =
            sorted2.OrderBy(e => e.bestTimeSeconds)
            .Take(maxShown)
            .ToList();

        // Spawn rows
        for (int i = 0; i < sorted3.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentParent);

            TMP_Text[] texts = row.GetComponentsInChildren<TMP_Text>();

            texts[0].text = (i + 1).ToString();                 // Rank
            texts[1].text = sorted3[i].username;                // Name
            texts[2].text = sorted3[i].bestTimeSeconds + " s";  // Time
        }
    }


}