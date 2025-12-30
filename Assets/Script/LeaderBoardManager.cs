using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections.Generic;

public class LeaderBoardManager : MonoBehaviour
{
    string path;
    public SaveTest saveTest = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
         path = Application.persistentDataPath + "/leaderboard.json";
         loadleaderboard();
    }

    public void saveleaderboard()
    {
        
        string json = JsonUtility.ToJson(saveTest, true);
        File.WriteAllText(path, json); 
    }

    public void loadleaderboard()
    {
        if (!File.Exists(path))
        {
            
            saveTest = new SaveTest();
          return;
        }
        string json = File.ReadAllText(path);

        saveTest = JsonUtility.FromJson<SaveTest>(json);

    }

    public void addscore(string name, string time)
    {
        saveTest.SaveData.Add(new Class
        {
            username = name,
            time = time


        });


    }


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
