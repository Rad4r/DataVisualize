using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private float score;
    private static DBManager DB;

    private void Start()
    {
        DB = FindObjectOfType<DBManager>();
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
