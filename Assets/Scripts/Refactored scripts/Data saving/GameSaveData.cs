using System.Collections.Generic;
using System.Collections;
using UnityEngine;


[System.Serializable]
public class GameSaveData
{
    public int currentWave;
    public int currentMoney;
   // public List<string> unlockedWeapons;

    public GameSaveData(int waveIndex, int money)
    {
        currentWave = waveIndex;
        currentMoney = money;
    }

}