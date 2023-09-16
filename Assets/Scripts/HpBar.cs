using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void setHp(int hp)
    {
        slider.value=hp;
    }

     public void setMaxHp(int hp)
    {
        slider.maxValue=hp;
        slider.value=hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
