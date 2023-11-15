using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : BaseBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    private GameStage gameStage;

    private float currentScore;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;
        SetGameStage(GameStage.MainMenu);
    }
    public enum GameStage
    {
        PLaying, MainMenu,
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
    public GameStage GetGameStage()
    {
        return gameStage;
    }
    public void SetGameStage(GameStage nextGameStage)
    {
        gameStage = nextGameStage;
        if(nextGameStage == GameStage.PLaying)
        {
            AudioManager.instance.StartPlayInGameSong();
        }
    }
    public void IncreaseScore(float value)
    {
        currentScore += value;
        GameCache.SetBestScore(currentScore, SongData.instance.GetCurrentSongID());
    }
    public float GetCurrentScore()
    {
        return currentScore;
    }
    public void StartGame()
    {
        SetGameStage(GameStage.PLaying);
    }
}
