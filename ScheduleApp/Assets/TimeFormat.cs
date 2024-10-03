using UnityEngine;
using TMPro;

public class TimeFormat : MonoBehaviour
{
    [SerializeField] private GameObject errorObj; //Maybe do in the manager actually after next pressed
    
    public int hours;
    public int minutes;

    private void Start()
    {
        hours = -1;
        minutes = -1;
    }

    public void UpdateHours(TMP_InputField hoursText) //Check if in right format - if nto then show error
    {
        int tempHour;
        if (int.TryParse(hoursText.text, out tempHour))
            hours = tempHour;
        else
            hours = -1;
    }
    
    public void UpdateMinutes(TMP_InputField minutesText)
    {
        int tempMinute;
        if (int.TryParse(minutesText.text, out tempMinute))
            minutes = tempMinute;
        else
            minutes = -1;
    }
}
