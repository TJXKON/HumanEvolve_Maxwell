using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Enemy bossHealth;
    public Slider slider;

    void Start()
    {
        slider.maxValue = bossHealth.currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = bossHealth.currentHP;
    }
}
