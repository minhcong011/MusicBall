using System;
using UnityEngine;

public static class GameCache
{
    public static void SetBestScore(float bestScore, int songID)
    {
        if (bestScore > GetBestScore(songID)) PlayerPrefs.SetFloat("BestScoreSong " + songID.ToString(), bestScore);
    }
    public static float GetBestScore(int songID)
    {
        return PlayerPrefs.GetFloat("BestScoreSong " + songID.ToString(), 0);
    }
}
