using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerScore
{
    static int currentScore;
    public static void IncreaseScore(int amount)
    {
        currentScore += amount;
    }

    public static int GetCurrentScore()
    {
        return currentScore;
    }
}
