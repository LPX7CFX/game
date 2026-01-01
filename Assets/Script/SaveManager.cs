using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public GameData data;

    private string path;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            path = Path.Combine(Application.persistentDataPath, "leaderboard.json");
            LoadOrCreate();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadOrCreate()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            data = new GameData();
            Save();
        }
    }

    public void SubmitScore(string username, int timeSeconds)
    {
        var entry = data.entries.Find(e => e.username == username);
        Debug.Log("Savetest11");

        if (entry == null)
        {
            Debug.Log("Savetest12");
            data.entries.Add(new LeaderboardEntry
            {
                username = username,
                bestTimeSeconds = timeSeconds
            });
            Debug.Log("Savetest13");
        }
        else if (timeSeconds < entry.bestTimeSeconds)
        {
            Debug.Log("Savetest14");
            entry.bestTimeSeconds = timeSeconds;
            Debug.Log("Savetest15");
        }
        Debug.Log("Savetest16");

        Save();
    }

    public void Save()
    {
        Debug.Log("Savetest17");
        string json = JsonUtility.ToJson(data, true);
        Debug.Log("Savetest18");
        File.WriteAllText(path, json);
        Debug.Log("Savetest19");
    }
}
