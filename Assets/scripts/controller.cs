using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class controller : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    public int totalDamageDoneByBoss = 0;
    public int totalDamageDoneToBoss = 0;
    private int tmpBosshealth;
    private int DamageDoneToBoss=0;
    private int DamageDoneByBoss=0;
    character warrior;
    character rogue;
    character mage;
    character moonkinDruid;
    Boss boss;
    characterPriest priest;
    private int timeSteps=0;

    private void Awake()
    {
        warrior = new character(3000, 5, 5);
        rogue = new character(1000, 20, 15);
        mage = new character(1000, 30, 1);
        moonkinDruid = new character(1000, 15, 5);
        boss = new Boss(4500, 20, 5, 55, 45);
        priest = new characterPriest(1000, 1000, 15, 20);

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[7];
        rowDataTemp[0] = "Time Step";
        rowDataTemp[1] = "BOSS";
        rowDataTemp[2] = "TANK";
        rowDataTemp[3] = "ROGUE";
        rowDataTemp[4] = "MAGE";
        rowDataTemp[5] = "MOONKIN DRUID";
        rowDataTemp[6] = "PRIEST";
        rowData.Add(rowDataTemp);
    }

    //edit this part of funciton to finish question 1
    private void FixedUpdate()
    {
        int boss_attact1;
        int boss_attact2;
        int boss_attact3;
        int boss_attact4;
        int boss_attact5;
        int healingChoice;
        int tmpDamageDoneToBoss;

        while (!anyOneDead())
        {
            //boss taken the damage from characters
            tmpBosshealth = boss.getCurrentHealth();
            tmpDamageDoneToBoss = warrior.damage() + rogue.damage() + mage.damage() + moonkinDruid.damage();
            boss.setCurrentHealth(tmpBosshealth-tmpDamageDoneToBoss);

            //boss damage to tank
            boss_attact1 = boss.damageToTank();
            warrior.setCurrentHealth(warrior.getCurrentHealth()-boss_attact1);

            //boss damage to others
            boss_attact2 = boss.damageToOthers();
            boss_attact3 = boss.damageToOthers();
            boss_attact4 = boss.damageToOthers();
            boss_attact5 = boss.damageToOthers();

            rogue.setCurrentHealth(rogue.getCurrentHealth() - boss_attact2);
            mage.setCurrentHealth(mage.getCurrentHealth() - boss_attact3);
            moonkinDruid.setCurrentHealth(moonkinDruid.getCurrentHealth() - boss_attact4);
            priest.setCurrentHealth(priest.getCurrentHealth() - boss_attact5);

            /*
             * Debugging infos
             * 
             */
            Debug.Log
                (
                "current health of boss is " + boss.getCurrentHealth() + " , it taken damage" + tmpDamageDoneToBoss + "\n" +
                "current health of tank is " + warrior.getCurrentHealth() + ", it taken damage" + boss_attact1 + "\n" +
                "current health of rogue is " + rogue.getCurrentHealth() + ", it taken damage" + boss_attact2 + "\n" +
                "current health of mage is " + mage.getCurrentHealth() + ", it taken damage" + boss_attact3 + "\n" +
                "current health of moonkinDruid is " + moonkinDruid.getCurrentHealth() + ", it taken damage" + boss_attact4 + "\n" +
                "current health of priest is " + priest.getCurrentHealth() + ", it taken damage" + boss_attact5 + "\n" 
                );

            /**
             * priest healing 
             * grab a random number from 0-4: 0,1,2,3,4; 
             * where 0,1 represent priest himself
             * 2 represent Rogue
             * 3 represent Mage
             * 4 represent Moonkin Druid
             * 
             * **/
            healingChoice = UnityEngine.Random.Range(0, 4+1);
            int healing = priest.castSmallHeal();

            if (healingChoice == 0 || healingChoice == 1)
            {
                priest.setCurrentHealth(priest.getCurrentHealth() + healing);
            }

            if(healingChoice == 2)
            {
                rogue.setCurrentHealth(rogue.getCurrentHealth() + healing);
            }

            if(healingChoice == 3)
            {
                mage.setCurrentHealth(mage.getCurrentHealth() + healing);
            }

            if(healingChoice == 4)
            {
                moonkinDruid.setCurrentHealth(moonkinDruid.getCurrentHealth() + healing);
            }

            //total damage calculating
            DamageDoneToBoss += tmpDamageDoneToBoss;
            DamageDoneByBoss += boss_attact1 + boss_attact2 + boss_attact3 + boss_attact4 + boss_attact5;

            /**
             * culmulate infomations for cvs
             * TIMESTEP
             * BOSS
             * TANK
             * ROGUE
             * MAGE
             * MOONKIN DRUID
             * PRIEST
             **/
            timeSteps += 1;
            string[] rowDataTemp = new string[7];
            rowDataTemp[0] = timeSteps.ToString();
            rowDataTemp[1] = boss.getCurrentHealth().ToString();
            rowDataTemp[2] = warrior.getCurrentHealth().ToString();
            rowDataTemp[3] = rogue.getCurrentHealth().ToString();
            rowDataTemp[4] = mage.getCurrentHealth().ToString();
            rowDataTemp[5] = moonkinDruid.getCurrentHealth().ToString();
            rowDataTemp[6] = priest.getCurrentHealth().ToString();
            rowData.Add(rowDataTemp);

        }
        //save info into the cvs file
        Save();

    }

    
    //if any character of Boss's currentHealth is 0, some one is dead, return ture.
    // else return false
    private bool anyOneDead()
    {
        if
            (
            warrior.getCurrentHealth() == 0 ||
            rogue.getCurrentHealth() == 0 ||
            mage.getCurrentHealth() == 0 ||
            moonkinDruid.getCurrentHealth() == 0 ||
            boss.getCurrentHealth() == 0 ||
            priest.getCurrentHealth() == 0
            )
            return true;

        else
            return false;
    }
    
    
    void Save()
    {

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }
}