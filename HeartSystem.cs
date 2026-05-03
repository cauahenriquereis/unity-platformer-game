using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HeartSystem : MonoBehaviour
{
   

    public Image healthBar;
    public float healthAmount = 100f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
       //if(Input.GetKeyDown(KeyCode.Space)) 
       //{
       //     TakeDamagge(20);
       //}
    }
    public void TakeDamagge(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;

    }
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Math.Clamp(healthAmount,0,100);

        healthBar.fillAmount = healthAmount / 100f;

    }
    public void DecreaseLife()
    {
        TakeDamagge(20f);
    }
    public void IncreaseLife()
    {
        Heal(100f);
    }
}
