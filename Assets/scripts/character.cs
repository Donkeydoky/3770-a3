
using UnityEngine;

public class character : ScriptableObject
{
    private int currentHealth;
    private int maxHealth;
    private int maxDamage;
    private int minDamage;

    public character(int maxHealth, int maxDamage, int minDamage)
    {
        setCurrentHealth(maxHealth);
        setMaxHealth(maxHealth);
        setMaxDamage(maxDamage);
        setMinDamage(minDamage);
    }

    public character()
    {
    }

    // the current health should not smaller than 0, larger than maxHealth.
    public void setCurrentHealth(int i)
    {
        if (i <= 0) { i = 0; }
        if (i >= maxHealth && maxHealth > 0) { i = this.maxHealth; }
        this.currentHealth = i;
        //Debug.Log("i = "+i); 
    }

    public void setMaxHealth(int i) { maxHealth = i; }
    public void setMaxDamage(int i) { maxDamage = i; }
    public void setMinDamage(int i) { minDamage = i; }

    //getter methods
    public int getCurrentHealth() { return currentHealth; }
    public int getMaxHealth() { return maxHealth; }
    public int getMaxDamage() { return maxDamage; }
    public int getMinDamage() { return minDamage; }


    // calling when object being damaged 
    public int damage()
    {
        return UnityEngine.Random.Range(minDamage, maxDamage+1);
    }

    // calling when being healed
    public void healed(int healed)
    {
        setCurrentHealth(currentHealth + healed);
    }

}

