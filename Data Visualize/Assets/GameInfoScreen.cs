using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameInfoScreen : MonoBehaviour
{

    [Header("Info Objects")]
    [SerializeField] private TextMeshProUGUI gameNameUI;
    [SerializeField] private TextMeshProUGUI versionNumbUI;
    [SerializeField] private TextMeshProUGUI dateUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private Image gameIcon;
    [SerializeField] private ImageDetail[] images;
    [SerializeField] private TextMeshProUGUI gameFound;
    
    [Header("Screen")] 
    [SerializeField] private GameObject menuScreen;
    private LoadObject gameData;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menuScreen.SetActive(true);
            gameObject.SetActive(false);
            Invoke("ResetButtonSelection", .1f);
        }
    }

    private void ResetButtonSelection()
    {
        EventSystem.current.SetSelectedGameObject(GameObject.Find("GamesContainer").transform.GetChild(0).gameObject);
    }

    public void UpdateGameInfo()
    {
        string game = EventSystem.current.currentSelectedGameObject.name;
        gameData = FindObjectOfType<LoadGameInfo>().FindData(game);
        menuScreen.SetActive(false);
        gameObject.SetActive(true);

        gameNameUI.text = gameData.gameName;
        versionNumbUI.text = gameData.versionNumber;
        dateUI.text = gameData.dateTime;
        highScoreUI.text = gameData.highScore.ToString();
        if (gameData.infoFound)
            gameFound.text = "Info found";
        else
            gameFound.text = "Info unavailable";
        
        foreach (ImageDetail img in images)
        {
            if (img.imageName == game)
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
