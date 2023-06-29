using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;

    public void UpdateHpBar(float maxHealth, float currentHealth)
    {
        hpBar.fillAmount = currentHealth / maxHealth;
    }
}
