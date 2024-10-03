using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleManager : MonoBehaviour
{
    [Header("Screen")] 
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject newScheduleScreen;
    [SerializeField] private GameObject dayChooseScreen;
    [SerializeField] private GameObject timeScreenOne;
    [SerializeField] private GameObject timeScreenTwo;
    [SerializeField] private GameObject activityScreenOne;
    [SerializeField] private GameObject activityScreenTwo;
    [SerializeField] private GameObject previewScheduleScreen;
    
    [SerializeField] private GameObject nextButtonInPreview;
    [SerializeField] private Image backButtonInPreview;
    
    [Header("Info Storage")] 
    [SerializeField] private TimeFormat[] Day1Times;
    [SerializeField] private TimeFormat[] Day2Times;
    [SerializeField] private TimeFormat[] Day3Times;

    [SerializeField] private TMP_Dropdown[] Day1Activities;
    [SerializeField] private TMP_Dropdown[] Day2Activities;
    [SerializeField] private TMP_Dropdown[] Day3Activities;//Couldbe not string
    
    private List<string> dayNameList;
    
    [Header("Info Display Components")]
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private GameObject errorMessageTwo;
    [SerializeField] private TextMeshProUGUI timeOne;

    [SerializeField] private TextMeshProUGUI[] DaysDisplayedInTime;
    [SerializeField] private TextMeshProUGUI[] DaysDisplayedInActivities;

    [SerializeField] private TextMeshProUGUI[] Day1TimesText;
    [SerializeField] private TextMeshProUGUI[] Day2TimesText;
    [SerializeField] private TextMeshProUGUI[] Day3TimesText;

    [Header("Schedule Preview Display")] //Need to be stored ina  file and loaded
    [SerializeField] private TextMeshProUGUI[] ScheduleDaysDisplay;
    [SerializeField] private TextMeshProUGUI[] ScheduleDay1TimesDisplay;
    [SerializeField] private TextMeshProUGUI[] ScheduleDay2TimesDisplay;
    [SerializeField] private TextMeshProUGUI[] ScheduleDay3TimesDisplay;
    
    [SerializeField] private TextMeshProUGUI[] ScheduleDay1ActivitiesDisplay;
    [SerializeField] private TextMeshProUGUI[] ScheduleDay2ActivitiesDisplay;
    [SerializeField] private TextMeshProUGUI[] ScheduleDay3ActivitiesDisplay;
    
    [Header(("ScheduleCheck"))] 
    public ScheduleInfo currentSchedule;

    // Start is called before the first frame update
    void Start()
    {
        backButtonInPreview.enabled = false; //set to just the image
        currentSchedule = new ScheduleInfo();
        dayNameList = new List<string>();
    }

    public void UpdateDaysInfo(List<string> dayNames) //could separate the day names and activities
    {
        
        // currentSchedule.dayOne = new DayInfo();
        dayNameList = dayNames;
        currentSchedule.dayOne.dayName = dayNames[0];
        currentSchedule.dayTwo.dayName = dayNames[1];
        currentSchedule.dayThree.dayName = dayNames[2];
        
        dayChooseScreen.SetActive(false);
        timeScreenOne.SetActive(true);
        UpdateDaysDisplayed(); //need to update function to be functional
    }

    public void NextTimePage()
    {
        bool errorOccured = false;
        
        foreach (TimeFormat time in Day1Times)
        {
            if (time.hours > 23 || time.hours < 1 || time.minutes > 59 || time.minutes < 0)
            {
                errorMessage.SetActive(true); //show error
                errorOccured = true;
                break;
            }
        }
        
        foreach (TimeFormat time in Day2Times)
        {
            if (time.hours > 23 || time.hours < 1 || time.minutes > 59 || time.minutes < 0)
            {
                errorMessage.SetActive(true);//show error
                errorOccured = true;
                break;
            }
        }
        
        //Disable error message and then return
        if(errorOccured)
            return;
        
        errorMessage.SetActive(false);
        timeScreenOne.SetActive(false);
        timeScreenTwo.SetActive(true);//Change to next page
    }
    
    public void NextTimePage2()
    {
        bool errorOccured = false;
        
        foreach (TimeFormat time in Day3Times)
        {
            if (time.hours > 23 || time.hours < 1 || time.minutes > 59 || time.minutes < 0)
            {
                errorMessage.SetActive(true); //show error
                errorOccured = true;
                break;
            }
        }
        
        //Disable error message and then return
        if(errorOccured)
            return;
        
        UpdateTimeInfo();
        errorMessageTwo.SetActive(false);
        timeScreenTwo.SetActive(false);
        activityScreenOne.SetActive(true);//Change to next page
    }
    
    void UpdateTimeInfo() ////Need to order it properly (Also maybe disable back for th etime being)
    {
        //Display time properly with the zeros
        
        for (int i = 0; i < dayNameList.Count; i++)
        {
            Day1TimesText[i].text = DisplayTimeFormat(Day1Times[i]);
            Day2TimesText[i].text = DisplayTimeFormat(Day2Times[i]);
            Day3TimesText[i].text = DisplayTimeFormat(Day3Times[i]);

            ScheduleDay1TimesDisplay[i].text = DisplayTimeFormat(Day1Times[i]);
            ScheduleDay2TimesDisplay[i].text = DisplayTimeFormat(Day2Times[i]);
            ScheduleDay3TimesDisplay[i].text = DisplayTimeFormat(Day3Times[i]);
        }
        UpdateDaysDisplayed(); //need to update function
        //Update local storage
    }

    private string DisplayTimeFormat(TimeFormat time)
    {
        string hourString;
        string minuteString;

        if (LengthOfInt(time.hours) == 1)
            hourString = "0" + time.hours;
        else
            hourString = "" + time.hours;
        
        if (LengthOfInt(time.minutes) == 1)
            minuteString = "0" + time.minutes;
        else
            minuteString = "" + time.minutes;
            
        return hourString + ":" + minuteString;
    }

    private int LengthOfInt(int number)
    {
        return number.ToString().Length;
    }

    void UpdateDaysDisplayed() //Need to order it properly
    {
        for (int i = 0; i < dayNameList.Count; i++)
        {
            DaysDisplayedInTime[i].text = dayNameList[i];
            DaysDisplayedInActivities[i].text = dayNameList[i];
            ScheduleDaysDisplay[i].text = dayNameList[i];
        }
    }

    public void NextActivityPage()
    {
        for (int i = 0; i < Day1Activities.Length; i++)
        {
            ScheduleDay1ActivitiesDisplay[i].text = Day1Activities[i].options[Day1Activities[i].value].text;
            ScheduleDay2ActivitiesDisplay[i].text = Day2Activities[i].options[Day2Activities[i].value].text;
        }
        
        activityScreenOne.SetActive(false);
        activityScreenTwo.SetActive(true);
    }
    
    public void NextActivityPage2() //save everything here maybe
    {
        for (int i = 0; i < Day3Activities.Length; i++)
            ScheduleDay3ActivitiesDisplay[i].text = Day3Activities[i].options[Day3Activities[i].value].text;
        
        activityScreenTwo.SetActive(false);
        newScheduleScreen.SetActive(false);
        previewScheduleScreen.SetActive(true);
        nextButtonInPreview.SetActive(true);
    }
    
    public void ReturnHome()
    {
        previewScheduleScreen.SetActive(false);
        backButtonInPreview.enabled = false;
        homeScreen.SetActive(true);
    }

    public void NewSchedule()
    {
        homeScreen.SetActive(false);
        newScheduleScreen.SetActive(true);
        dayChooseScreen.SetActive(true);
    }
    
    public void PreviewSchedule()
    {
        homeScreen.SetActive(false);
        backButtonInPreview.enabled = true;
        previewScheduleScreen.SetActive(true);
        nextButtonInPreview.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    // void UpdateSchedulePreview() //update with all the info
    // {
    //     
    // }
}

public struct ScheduleInfo // 
{
    // public DayInfo[] daysInfo;
    public DayInfo dayOne;
    public DayInfo dayTwo;
    public DayInfo dayThree;
}

public struct DayInfo //need name of day
{
    public string dayName;
    public TimeActivity[] TimeActivities;
    /*public string dayOne;
    public TimeActivity[] dayOneTimes; // should have 3 of each
    public TimeActivity[] dayTwoTimes;
    public TimeActivity[] dayThreeTimes;*/
}

/*public struct DayActivities
{
    public string dayName;
    public TimeActivity[] TimeActivities;
}*/

public struct TimeActivity
{
    public string time;
    public string activity;
}

public struct DaysAndTime
{
    public string timeOne;
    public string activityOne;
    public string timeTwo;
    public string activityTwo;
    public string timeThree;
    public string activityThree;
}