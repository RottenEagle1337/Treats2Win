using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int bestScore;

    public PlayerData(PlayerController player)
    {
        bestScore = player.candies;
    }
}
