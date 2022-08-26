using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarEnemy : MonoBehaviour
{
    public Slider healthBar;
    public Color Low;
    public Color High;
    public Vector3 offSet;

    public void SetHealth(float health, float maxHealth)
    {
        healthBar.gameObject.SetActive(health < maxHealth);
        healthBar.value = health;
        healthBar.maxValue = maxHealth;

        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, healthBar.normalizedValue);
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
    }
}
