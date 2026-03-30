using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Set the maximum health
    public void SetMaxHealth(int health)
    {
        if (slider != null)
        {
            slider.maxValue = health;
            slider.value = health;
        }
    }

    // Update current health
    public void SetHealth(int health)
    {
        if (slider != null)
        {
            slider.value = health;
        }
    }
}