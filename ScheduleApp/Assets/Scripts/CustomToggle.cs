using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    [SerializeField] private bool isSelected;
    [SerializeField] private Color backgroundOnColor;

    private static ToggleController TG;
    private Image backgroundImage;
    private Color backgroundOffColor;
    
    void Start()
    {
        TG = GetComponentInParent<ToggleController>();
        backgroundImage = GetComponent<Image>();
        backgroundOffColor = Color.black;
        backgroundImage.color = backgroundOffColor; //Color is off by default
    }

    public void ChangeToggle()
    {
        //if max reached then only se to false otherwise toggle

        if (TG.GetDaysSelected() >= 3) 
        {
            if (isSelected) //need to do if not at max either
                isSelected = false;
            else
                return;
        }
        else
            isSelected = !isSelected;
        
        if (isSelected)
        {
            backgroundImage.color = backgroundOnColor;
            TG.AddDay(gameObject.name);
        }
        else
        {
            backgroundImage.color = backgroundOffColor;
            TG.RemoveDay(gameObject.name);
        }

    }
}
