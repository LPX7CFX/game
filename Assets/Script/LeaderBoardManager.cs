using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public Transform contentParent;   // Where rows will be spawned
    public GameObject rowPrefab;      // One leaderboard row prefab
    public int maxShown = 10;

    void OnEnable()
    {
        ShowLeaderboard();
    }

    public void ShowLeaderboard()
    {
        if (SaveManager.Instance == null) return;
        if (SaveManager.Instance.data == null) return;

        // Clear old rows
        for (int i = contentParent.childCount - 1; i >= 0; i--)
        {
            Destroy(contentParent.GetChild(i).gameObject);
        }

        // Sort by LOWEST time (best first)
        List<LeaderboardEntry> sorted =
            SaveManager.Instance.data.entries
            .OrderBy(e => e.bestTimeSeconds)
            .Take(maxShown)
            .ToList();

        // Spawn rows
        for (int i = 0; i < sorted.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentParent);

            TMP_Text[] texts = row.GetComponentsInChildren<TMP_Text>();

            texts[0].text = (i + 1).ToString();                 // Rank
            texts[1].text = sorted[i].username;                // Name
            texts[2].text = sorted[i].bestTimeSeconds + " s";  // Time
        }
    }
}