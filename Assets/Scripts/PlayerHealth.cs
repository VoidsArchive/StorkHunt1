using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int currentHealth = GameParameters.PlayerStartingHealth;

    public int GetPlayerHealth()
    {
        return currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = GameParameters.PlayerStartingHealth;
    }
    public static void ChangeHealth(int amount)
    { 
        currentHealth += amount;
       if (currentHealth > GameParameters.PlayerStartingHealth)
       { 
           currentHealth = GameParameters.PlayerStartingHealth;
        }
    }

    public string HealthAsString()
    {
        return ("HP: " + currentHealth + "/" + GameParameters.PlayerStartingHealth);
    }
    
}
