using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public Class currentData;

    void Awake()
    {
        // Singleton (one save manager only)
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    string GetPath(string username)
    {
        return Application.persistentDataPath + "/" + username + ".json";
    }

    public bool HasSave(string username)
    {
        return File.Exists(GetPath(username));
    }

    public void CreateNew(string username)
    {
        currentData = new Class
        {
            username = username,
            time = "0"
        };

        Save(username);
    }

    public void Load(string username)
    {
        string json = File.ReadAllText(GetPath(username));
        currentData = JsonUtility.FromJson<Class>(json);
    }

    public void Save(string username)
    {
        string json = JsonUtility.ToJson(currentData, true);
        File.WriteAllText(GetPath(username), json);
    }
}