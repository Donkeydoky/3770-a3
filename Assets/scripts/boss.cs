using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : character
{

    private int maxDamage2;
    private int minDamage2;

    //maxDamage1&minDamage1 for the others
    //maxDamage2&minDamage2 for the tank
    public Boss(int maxHealth, int maxDamage1, int minDamage1, int maxDamage2, int minDamage2)
    {
        setMaxDamage(maxDamage1);
        setMaxHealth(maxHealth);
        setCurrentHealth(maxHealth);
        setMinDamage(minDamage1);
        setMaxDamage2(maxDamage2);
        setMinDamage2(minDamage2);

    }

    void setMaxDamage2(int i) { maxDamage2 = i; }
    void setMinDamage2(int i) { minDamage2 = i; }

    public int damageToOthers()
    {
        return damage();
    }

    public int damageToTank()
    {
        return UnityEngine.Random.Range(minDamage2, maxDamage2+1);
    }

 }
