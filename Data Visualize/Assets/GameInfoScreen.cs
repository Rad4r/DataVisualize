using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoScreen : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI gameNameUI;
    [SerializeField] private TextMeshProUGUI versionNumbUI;
    [SerializeField] private TextMeshProUGUI dateUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private Image gameIcon;
    [SerializeField] private ImageDetail[] images;
    
    public void UpdateGameInfo(string gameName) //could just use the gameObject.Name
    {
        LoadObject gameData = FindObjectOfType<LoadGameInfo>().FindData(gameName);

        gameNameUI.text = gameData.gameName;
        versionNumbUI.text = gameData.versionNumber;
        dateUI.text = gameData.dateTime;
        highScoreUI.text = gameData.highScore.ToString();

        foreach (ImageDetail img in images)
        {
            if (img.imageName == gameName)
            {
                gameIcon.sprite = img.imageSprite;
                break;
            }
        }
    }
    
}

[Serializable]
public class ImageDetail
{
     public string imageName;
     public Sprite imageSprite;
}
