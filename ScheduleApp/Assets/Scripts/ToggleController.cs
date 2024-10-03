using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
    private static ScheduleManager SI;
    
    private CustomToggle[] customtoggles;
    // private int selectedAmount;
    private List<string> selectedDays;

    // Start is called before the first frame update
    void Start()
    {
        selectedDays = new List<string>();
        customtoggles = GetComponentsInChildren<CustomToggle>();
        SI = FindObjectOfType<ScheduleManager>();
    }

    public void UpdateDates() //Could order the list here before sending it
    {
        if(selectedDays.Count < 3) //Max Days Not Reached
            return;
        SI.UpdateDaysInfo(selectedDays);
        
        // gameObject.SetActive(false); // change to next screen here
        
        //need to get the names from the button
        //Add current button to a list and check size of that instead
        
        //Upate the schedule manager here with the selected dates
        //Additionally maybe add code that lets you pick more days and flexibly adjust the amount of pages with 
        // how many days selected (Will have to adjust schedule preview as well then)
    }

    public void AddDay(string dayName)
    {
        selectedDays.Add(dayName);
    }

    public void RemoveDay(string dayName)
    {
        selectedDays.Remove(dayName);
    }
    
    public int GetDaysSelected()
    {
        return selectedDays.Count;
    }

    /*public void UpdatetogglesSelected(int addNumb)
    {
        selectedAmount += addNumb;
        Debug.Log("Amount selected: " + selectedAmount);
    }
    
    public int GetTogglesSelected()
    {
        return selectedAmount;
    }*/
}
