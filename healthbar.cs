using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime;

public class healthbar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetSlider(float amount)
    {
        healthSlider.value = amount;

    }

    public void SetSliderMax(float amount){
        healthSlider.maxValue= amount;
        SetSlider(amount);
    }
}
