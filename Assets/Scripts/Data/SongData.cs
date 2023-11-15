using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData : BaseBehaviour
{
    public static SongData instance;
    [SerializeField] private SongSheet[] song;
    private SongSheet currentSong;
    private int currentSongID;
    public override void Awake()
    {
        instance = this;
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        SetCurrentSong(0);
    }

    public override void Update()
    {
        base.Update();
    }
    public SongSheet GetCurrentSong()
    {
        return currentSong;
    }
    public int GetCurrentSongID()
    {
        return currentSongID;
    }
    public void SetCurrentSong(int id)
    {
        currentSongID = id;
        currentSong = song[id];
    }
}
