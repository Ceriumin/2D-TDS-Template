using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [Header( "Prefabs" )]
    public Text healthText;
    public Image healthBar;

    [Header( "Statistics" )]
    public static float health, maxHealth = 100;
    float lerpSpeed;

    void Start()
    {
        health = maxHealth;     
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();
        ColourChanger();
    }

    void HealthBarFiller()
    {
        healthText.text = health + "%";
        lerpSpeed = 3f * Time.deltaTime;
        if (health > maxHealth) health = maxHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    void ColourChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;

        if (health == 0)
        {
            Debug.Log("Player has Died");
        }         
    }

    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
}
