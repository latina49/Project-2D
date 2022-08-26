using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public CharacterControl playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
        healthBar = GetComponent<Slider>();
        if (healthBar.gameObject.tag == "Health")
        {
            healthBar.maxValue = playerHealth.maxHealth;
            healthBar.value =playerHealth.curHealth;
        }
        if (healthBar.gameObject.tag == "Mana")
        {
            healthBar.maxValue = 75;
            healthBar.value = playerHealth.mana;
        }
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
}

