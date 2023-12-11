using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfantryHealthBar : MonoBehaviour
{
    [SerializeField] int maxHealth;
    
    Slider slider;
    BaseInfantryHealth infantryHealth;

    void OnEnable() 
    {
        slider = GetComponent<Slider>();
        infantryHealth = GetComponentInParent<BaseInfantryHealth>();

        maxHealth = infantryHealth.Health;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void UpdateEnemyHealthBar(int healthRem)
    {
        slider.value = healthRem;
    }
}
