using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMan : MonoBehaviour
{
    public Image healthBar;
    public float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 100;
        maxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamagePlayer();
        }

        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void DamagePlayer()
    {
        currentHealth = currentHealth - 10;
    }
}
