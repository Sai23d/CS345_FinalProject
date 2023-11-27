using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    LevelManager levelManager;
    int score;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>(); // Find the LevelManager in the scene
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)
    {
        score += value;
        score = Mathf.Clamp(score, 0, int.MaxValue); // Clamp the score value
        Debug.Log(score);
    }

    public void Reset()
    {
        score = 0;
    }
}
