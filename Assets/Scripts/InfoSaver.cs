using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

public class InfoSaver : MonoBehaviour
{
    class FileWork
    {
        public FileWork()
        {
            this.Load();
        }

        public void Load()
        {
            string tempPath = Path.Combine(Application.persistentDataPath, "data");
            tempPath = Path.Combine(tempPath, "data.txt");

            //Exit if Directory or File does not exist
            if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
            {
                Debug.LogWarning("Directory does not exist");
                return;
            }

            if (!File.Exists(tempPath))
            {
                Debug.Log("File does not exist");
                return;
            }

            //Load saved Json
            byte[] jsonByte = null;
            try
            {
                jsonByte = File.ReadAllBytes(tempPath);
                Debug.Log("Loaded Data from: " + tempPath.Replace("/", "\\"));
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
                Debug.LogWarning("Error: " + e.Message);
            }

            //Convert to json string
            string jsonData = Encoding.ASCII.GetString(jsonByte);

            //Convert to Object
            object resultValue = JsonUtility.FromJson<SaveData>(jsonData);
            InfoSaver.SavedData = (SaveData)Convert.ChangeType(resultValue, typeof(SaveData));
        }

        public void Save()
        {

            string tempPath = Path.Combine(Application.persistentDataPath, "data");
            tempPath = Path.Combine(tempPath, "data.txt");

            //Convert To Json then to bytes
            string jsonData = JsonUtility.ToJson(InfoSaver.SavedData, true);
            byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);

            //Create Directory if it does not exist
            if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
            }
            //Debug.Log(path);

            try
            {
                File.WriteAllBytes(tempPath, jsonByte);
                Debug.Log("Saved Data to: " + tempPath.Replace("/", "\\"));
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed To PlayerInfo Data to: " + tempPath.Replace("/", "\\"));
                Debug.LogWarning("Error: " + e.Message);
            }
        }
    }

    [Serializable]
    public class SaveData
    {
        public int MaxDistance = 0;
        public int Money = 0;
        public bool isSeenPlayer = false;
        public bool isSeenCannon = false;
        public bool isSeenBonus = false;

        public int ShieldStat = 0;
        public int DisableVoltageStat = 0;
        public int FreezeStat = 0;
    }

    private static FileWork file;
    public static SaveData SavedData;

    public static void Save()
    {
        file.Save();
    }

    public static void Load()
    {
        file.Load();
    }

    void Start()
    {
        SavedData = new SaveData();
        file = new FileWork();
    }
}
