using System.Collections.Generic;
using System.Collections;
using UnityEngine;


[System.Serializable]
public class GameSaveData
{
    public int currentWave;
    public int money;
   // public List<string> unlockedWeapons;

    public GameSaveData(int waveIndex, int money)
    {
        currentWave = waveIndex;

    }

}