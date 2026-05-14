using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth = GameParameters.PlayerStartingHealth;
    
    public TMP_Text healthText;
    
    
    void Start()
    {
        UpdateHealth();
    }

    public void ChangeHealth(int amount)
    { 
        currentHealth += amount;
       if (currentHealth > maxHealth)
       { 
           currentHealth = maxHealth;
        } 
       
        UpdateHealth();

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

       
    }

    public void UpdateHealth()
    {
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }
    
}
