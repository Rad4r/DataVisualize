using UnityEngine;
using System.IO;
using System;

public class LoadGameInfo : MonoBehaviour
{
    System.Threading.Thread m_LoadThread = null;
    System.Threading.Thread m_SaveThread = null;

    string m_DataPath;

    private float highScoreValue;
    private int lineToEdit = -1;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_DataPath = Application.persistentDataPath + "/../YellowRidersHospitalGame.txt";
        FindLineToAdd();

        m_SaveThread = new System.Threading.Thread(SaveThread);
    }

    public void SaveData(float score)
    {
        highScoreValue = score;
        SaveThread();
        print(m_DataPath);
    }

    private void SaveThread()
    {
        LoadObject saveObject = new LoadObject
        {
            gameName = Application.productName, // at the start
            versionNumber = Application.version + " build 1", // at the start
            dateTime = DateTime.Now.ToString(), //save when the application closed or lost focus
            highScore = highScoreValue, //check every so often and update of higher than current (need to load the current one)
        };
    
        string json = JsonUtility.ToJson(saveObject);
        if (lineToEdit < 0)
        {
            File.AppendAllText(m_DataPath,  json); //write new line
            FindLineToAdd();
        }
        else
            LineUpdate(json); //update existing line
    
        m_SaveThread = new System.Threading.Thread(SaveThread);
    }

    void FindLineToAdd()
    {
        string[] fileLines = File.ReadAllLines(m_DataPath);
        
        for (int i = 0; i <= fileLines.Length -1 ; i++) //check only once in the beginning
        {
            if (fileLines[i].Contains(Application.productName))
                lineToEdit = i;
        }
    }

    void LineUpdate(string json) // COULD UPDATE THE TEXT AND ADD THE NEW TEXT AS A WRITE ALL TO THE FILE with fileLines
    {
        //FindLineToAdd();
        string[] fileLines = File.ReadAllLines(m_DataPath);
        fileLines[lineToEdit] = json;
        File.WriteAllLines(m_DataPath, fileLines);
    }
    
}

[Serializable]
public struct LoadObject
{
    public string gameName;
    public string versionNumber;
    public string dateTime;
    public float highScore;
}

//Player_ID, Game_ID, Level_ID, Version_ID, DateTime_ID, Score_ID

// void OnStateUpdate()
// {
//     label.text = Value.ToString();
//     Invoke("SaveHighScore",.1f);
// }
//
// void SaveHighScore()
// {
//     FindObjectOfType<SaveManager>().SaveData(currentGameState.TopScore);
// }

