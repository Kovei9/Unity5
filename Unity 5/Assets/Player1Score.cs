using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private int score = 0;

    public void IncrementScore()
    {
        score++;
    }

    public int GetScore()
    {
        return score;
    }
}