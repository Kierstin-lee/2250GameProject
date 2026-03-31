using UnityEngine;
using UnityEngine.UI;

//method to set the boss health bar that appears above the character - level 6 (Kierstin)
public class BossHealthBar : MonoBehaviour
{
    //This is the health bar, queen of adaptation right here 
    [SerializeField] private Slider slider;

    //set the max health on the slider (bar is full)
    public void SetMaxHealth(int health)
    {
        if (slider != null)
        {
            slider.maxValue = health;
            slider.value = health;
        }
    }

    //as the boss takes damage, update the health bar (bar becomes less full)
    public void SetHealth(int health)
    {
        if (slider != null)
        {
            slider.value = health;
        }
    }
}