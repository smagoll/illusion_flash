using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void SetValue(float value)
    {
        slider.value = value;
    }

    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }
}