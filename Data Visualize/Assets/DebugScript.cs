using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.tvOS;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI advertIDText;
    private float score;
    private static DBManager DB;

    private void Start()
    {
        DB = FindObjectOfType<DBManager>();
        advertIDText.text = Device.advertisingIdentifier;
        SetScore();
        // score = DB.LoadScore().GetAwaiter();
    }

    private async void SetScore()
    {
        score = await DB.LoadScore();
        UIScoreUpdate();
    }

    private void UIScoreUpdate()
    {
        scoreText.text = score.ToString();
    }

    public void IncreaseScore()
    {
        score++;
        DB.UpdateHighscore(score); // Make an action and when it's invoked do this? OR delegate
        UIScoreUpdate();
    }
    
    public void DecreaseScore()
    {
        score--;
        DB.UpdateHighscore(score);
        UIScoreUpdate();
    }
}
