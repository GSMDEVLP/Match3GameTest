using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    private int _score = 0;

    public int GetScore()
    {
        return _score;
    }
    public void UpdateScore(int points)
    {
        _score += points;
    }
}
