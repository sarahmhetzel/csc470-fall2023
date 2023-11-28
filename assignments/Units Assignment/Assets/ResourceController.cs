using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{

    public static float currentLevel;
    public float maxLevel;
    public float increaseRate;

    public Image fillImage;
    public Slider resourceSlider;

    private void Awake()
    {
        resourceSlider = GetComponent<Slider>();
        currentLevel = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fillValue = currentLevel;
        currentLevel = currentLevel + increaseRate * Time.deltaTime;
        resourceSlider.value = fillValue;
    }
}
