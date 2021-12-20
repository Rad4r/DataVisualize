using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    // System.Threading.Thread m_SaveThread = null;
    System.Threading.Thread m_LoadThread = null;

    string m_DataPath;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // m_SaveThread = new System.Threading.Thread(SaveThread);
        print(Application.persistentDataPath);

        m_DataPath = Application.persistentDataPath + "/YellowRidersHospitalGame.txt";

        m_LoadThread = new System.Threading.Thread(LoadData);
        m_LoadThread.Start();

        // DataManager.m_SaveLoad = this;
    }

    public void LoadData()
    {
        if (File.Exists(m_DataPath))
        {
            string saveString = File.ReadAllText(m_DataPath);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            // DataManager.m_MasterVolume = saveObject.m_MasterVolume;
            // DataManager.m_MusicVolume = saveObject.m_MusicVolume;
            // DataManager.m_SFXVolume = saveObject.m_SFXVolume;
        }
        else
        {
            // DataManager.m_MasterVolume = 1;
            // DataManager.m_MusicVolume = 0.1f;
            // DataManager.m_SFXVolume = 1;
        }

        m_LoadThread = new System.Threading.Thread(LoadData);
    }
    
    public bool GetLoadThread()
    {
        return m_LoadThread.IsAlive;
    }
    
    // public void SaveData()
    // {
    //     if (m_SaveThread.IsAlive == false)
    //     {
    //         m_SaveThread.Start();
    //     }
    //
    //     print(m_DataPath);
    // }

    // private void SaveThread()
    // {
    //     SaveObject saveObject = new SaveObject
    //     {
    //         m_MasterVolume = DataManager.m_MasterVolume,
    //         m_MusicVolume = DataManager.m_MusicVolume,
    //         m_SFXVolume = DataManager.m_SFXVolume,
    //     };
    //
    //     string json = JsonUtility.ToJson(saveObject);
    //     File.WriteAllText(m_DataPath, json);
    //
    //     m_SaveThread = new System.Threading.Thread(SaveThread);
    // }
    //
}

[Serializable]
public struct SaveObject
{
    public float m_MasterVolume;
    public float m_MusicVolume;
    public float m_SFXVolume;
}