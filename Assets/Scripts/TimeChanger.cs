using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChanger : MonoBehaviour
{
    public GameObject clocktower;
    TimeManager manager;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        manager = clocktower.GetComponent<TimeManager>();
        if (manager is null) Debug.LogError("Time manager component not present");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ValueChangeCheck()
    {
        float x = slider.value;
        manager.speed = x;
    }
}
