using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/save.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // DATA CLASS (AQUI AGREGAS LO QUE QUIERAS GUARDAR)
    [System.Serializable]
    public class SaveData
    {
        public int _pesos;
        public int _totalChests;
        public int _gamesPlayed;

        public int[] _loteriaCardsID;
     
        public bool _cardSet;

        public bool[] _cardMarked = new bool[16];
        public bool[] _lineTriggered = new bool[12];
    }

    public SaveData data = new SaveData();

    // GUARDAR
    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("GAME SAVED: " + savePath);
    }

    // CARGAR
    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("SAVE LOADED");
        }
        else
        {
            Debug.Log("NO SAVE FOUND, CREATING NEW");
            Save();
        }
    }

    // BORRAR SAVE
    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("SAVE DELETED");
        }
    }
}