using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.tvOS;

public class DBManager : MonoBehaviour
{
    FirebaseDatabase database;

    private const string INFOKEY = "DEBUG";
    private DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    private LoadObject newDebugInfo;

    private void Start()
    {
        float time = Time.time;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
                Debug.Log("Check Done! (Done within " + (Time.time-time) + " seconds)");
            else
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus); //Display it somewhere else
        });
    }

    private void UpdateDebugScreen(int score)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log("CHECKING");
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
                UpdateHighscore(score); // bool or some thing here
            else
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus); //Display it somewhere else
        });
    }

    public void UpdateHighscore(float highScore)
    {
        database = FirebaseDatabase.DefaultInstance;
        
        newDebugInfo = new LoadObject()
        {
            gameName = Application.productName,
            versionNumber = Application.version,
            dateTime = DateTime.Now.ToString(),
            highScore = highScore
        };

        string json = JsonUtility.ToJson(newDebugInfo);
        
        if (Device.advertisingIdentifier == "")
            database.GetReference("testPC").Child(INFOKEY).SetRawJsonValueAsync(json);
        else
            database.GetReference(Device.advertisingIdentifier).Child(INFOKEY).SetRawJsonValueAsync(json); //advertising ID could eb blank on wndows and needs setup on app store connect
        
        Debug.Log("Info Save on Database");

    }
    
    public async Task<float> LoadScore()
    {
        database = FirebaseDatabase.DefaultInstance;

        //test pc or adID
        DataSnapshot reference = await database.GetReference("testPC").Child(INFOKEY).GetValueAsync();

        if (reference.Exists)
        {
            LoadObject gameInfo = JsonUtility.FromJson<LoadObject>(reference.GetRawJsonValue());
            return gameInfo.highScore;
        }
        Debug.LogError("Could not find score");
        return 0;
    }

    public async Task<LoadObject> LoadInfo(string gameName)
    {
        database = FirebaseDatabase.DefaultInstance;

        //test pc or adID
        DataSnapshot reference = await database.GetReference("testPC").Child(gameName).GetValueAsync();

        if (reference.Exists)
        {
            Debug.Log("Info Found");
            LoadObject gameInfo = JsonUtility.FromJson<LoadObject>(reference.GetRawJsonValue());
            gameInfo.infoFound = true;
            return gameInfo;
        }
        if (gameName == "DEBUG")
        {
            Debug.Log("INFO NOT FOUND SO NEW created");
            UpdateHighscore(0); //wait for this then 
            return newDebugInfo;
        }
        
        Debug.LogError("Could not find this data");
        return new LoadObject(){gameName = "default", dateTime = "00/00/00", highScore = 0,versionNumber = "0", infoFound = false};
    }

    public async Task<bool> SaveExists()
    {
        DataSnapshot dataSnapShot = await database.GetReference(INFOKEY).GetValueAsync();
        return dataSnapShot.Exists;
    }

    public void EraseSave()
    {
        database.GetReference(INFOKEY).RemoveValueAsync();
    }
    
    // Start is called before the first frame update
    // public void UpdateDatabase()
    // {
    //     // DataSnapshot = 
    //     return;
    // }
}


// public struct LoadObject
// {
//     public string gameName;
//     public string versionNumber;
//     public string dateTime;
//     public float highScore;
//     public bool infoFound;
// }

// if (task.Exception != null)
// {
//     Debug.Log($"Error Firebase app exception: {task.Exception}");
//     return;
// }
//     
// // OnFireBaseIntialized.Invoke()
// InitializeDatabase();