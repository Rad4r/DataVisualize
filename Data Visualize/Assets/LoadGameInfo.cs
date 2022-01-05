using UnityEngine;
using System.IO;
using System;
using TMPro;

public class LoadGameInfo : MonoBehaviour
{
    System.Threading.Thread m_LoadThread = null;

    [SerializeField] private TextMeshProUGUI filePath;

    string m_DataPath;

    private float highScoreValue;
    private int lineToEdit = -1;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_DataPath = Application.persistentDataPath + "/../YellowRidersHospitalGame.txt";
        filePath.text = "File Path: " + m_DataPath;
        //m_LoadThread = new System.Threading.Thread(LoadThread);
    }

    public LoadObject FindData(string gameName) // need the string to check
    {
        if (File.Exists(m_DataPath))
        {
            string[] fileLines = File.ReadAllLines(m_DataPath);
        
            for (int i = 0; i <= fileLines.Length -1 ; i++) //check only once in the beginning
            {
                if (fileLines[i].Contains(gameName))
                {
                    LoadObject gameInfo = JsonUtility.FromJson<LoadObject>(fileLines[i]);
                    gameInfo.infoFound = true;
                    return gameInfo;
                }
            }
        }
        return new LoadObject(){gameName = "default", dateTime = "00/00/00", highScore = 0,versionNumber = "0", infoFound = false};
    }
    
}

[Serializable]
public struct LoadObject
{
    public string gameName;
    public string versionNumber;
    public string dateTime;
    public float highScore;
    public bool infoFound;
}



