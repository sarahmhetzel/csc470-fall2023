using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private float maxOxygen;

    private float currentOxygen;
    public OxygenBar oxygenBar;
    public float airDecrease;

    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = maxOxygen;

        oxygenBar.SetSliderMax(maxOxygen);
    }

    // Update is called once per frame
    void Update()
    {
        float fillValue = currentOxygen;
        currentOxygen = currentOxygen - airDecrease * Time.deltaTime;
        oxygenBar.SetSlider(fillValue);

        if(currentOxygen > maxOxygen)
        {
            currentOxygen = maxOxygen;
        }

        if (currentOxygen <= 0)
        {
            Die();
        }
    }

    public void CollectOxygen(float amount)
    {
        currentOxygen += amount;
        oxygenBar.SetSlider(currentOxygen);
    }

    private void Die()
    {
        SceneManager.LoadScene("LoseScreen");
    }
}
