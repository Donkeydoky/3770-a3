
using UnityEngine;

public class characterPriest : character
{
    private int maxMana;
    private int currentMana;
    private int smallHeal;
    private int smallHealCost = 5;
    private int bigHealcost = 8;
    private int bigHeal;
    private int manaRegenerate=2;

    public characterPriest(int health,int mana, int smallHeal, int bigHeal)
    {
        setMaxHealth(health);
        setCurrentHealth(health);
        maxMana = mana;
        currentMana = mana;
        this.smallHeal = smallHeal;
        this.bigHeal = bigHeal;
    }

    public void updateMana(int i)
    {
        if (i >= maxMana) { i = maxMana; }
        if (i <= 0) { i = 0; }
            currentMana = i;
    }

    public int castSmallHeal() { updateMana(currentMana - smallHealCost); return smallHeal; }

    // int i = 0, this big heal cost 0 mana; for scene level3;
    public int castBigHeal(int i) { if(i != 0) { updateMana(currentMana - bigHealcost); } return bigHeal; }
  
}
