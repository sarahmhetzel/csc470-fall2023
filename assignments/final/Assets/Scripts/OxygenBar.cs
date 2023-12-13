using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{

    public Slider oxygenSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlider(float amount)
    {
        oxygenSlider.value = amount;
    }

    public void SetSliderMax(float amount)
    {
        oxygenSlider.maxValue = amount;
        SetSlider(amount);
    }
}
