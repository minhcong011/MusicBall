using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapPosManager : MonoBehaviour
{
    public static TapPosManager instance;
    [SerializeField] private GameObject tapPosPref;
    [SerializeField] private Color32 colorTapPosNearPlayer;
    [SerializeField] private Color32 colorTapPosTwoNearPlayer;
    private int currentKey;
    private float countDown;
    private float length = 0;
    private List<GameObject> listTapPos = new();

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentKey = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetGameStage() != GameManager.GameStage.PLaying) return;
        ManageSpawnTapPos();
        ManageTapPosColor();
        Debug.Log(listTapPos[0].GetComponent<TapPosController>().isDoubleTap);
    }
    public GameObject GetTapPosInList(int id)
    {
        return listTapPos[id];
    }
    public List<GameObject> GetListTapPos()
    { 
        return listTapPos ; 
    }
    private void ManageSpawnTapPos()
    {
        countDown += Time.deltaTime;
        if (countDown >= length)
        {
            if (!SongData.instance.GetCurrentSong().keyArray[currentKey].isDoubleNote)
            {
                Vector2 posSpawn = GetRandSpawnPos();
                if (SongData.instance.GetCurrentSong().keyArray[currentKey].permanentSide)
                {
                    if (SongData.instance.GetCurrentSong().keyArray[currentKey].side == 0)
                    {
                        if (posSpawn.x > 0) posSpawn *= new Vector2(-1, 1);
                    }
                    else
                    {
                        if (posSpawn.x < 0) posSpawn *= new Vector2(-1, 1);
                    }
                }
                SpawnTapPos(posSpawn, false);
            }
            else
            {
                Vector2 posSpawn = GetRandSpawnPos();
                SpawnTapPos(posSpawn, true);
                posSpawn *= new Vector2(-1, -1);
                SpawnTapPos(posSpawn, true);
            }
            countDown = 0;
            length = SongData.instance.GetCurrentSong().keyArray[currentKey].length;
            currentKey++;
        }
        if (currentKey >= SongData.instance.GetCurrentSong().keyArray.Length) currentKey = 0;
    }
    private Vector2 GetRandSpawnPos()
    {
        int randSide = Random.Range(0, 6);
        Vector2 randPos = Vector2.zero;
        switch (randSide)
        {
            case 0: randPos = new(Random.Range(-8, -4), 8); break;
            case 1: randPos = new(Random.Range(-8, -4), -8); break;
            case 2: randPos = new(Random.Range(4, 8), -8); break;
            case 3: randPos = new(Random.Range(4, 8), -8); break;
            case 4: randPos = new(8, Random.Range(-8, 8)); break;
            case 5: randPos = new(-8, Random.Range(-8, 8)); break;
        }
        return randPos;
    }
    private void SpawnTapPos(Vector2 spawnPos, bool doubleTap)
    {
        GameObject newTapPos = Instantiate(tapPosPref);
        if (!doubleTap) newTapPos.GetComponent<TapPosController>().SetTapPos(spawnPos);
        else newTapPos.GetComponent<TapPosController>().SetDoubleTapPos(spawnPos);
        listTapPos.Add(newTapPos);
    }
    private void ManageTapPosColor()
    {
        if (listTapPos.Count == 0) return;
        if (!GetTapPosInList(0).GetComponent<TapPosController>().isDoubleTap)
        {
            ChangeTapPosColor(GetTapPosInList(0), colorTapPosNearPlayer);
            if (GetTapPosInList(1).GetComponent<TapPosController>().isDoubleTap)
            {
                ChangeTapPosColor(GetTapPosInList(1), colorTapPosTwoNearPlayer);
                ChangeTapPosColor(GetTapPosInList(2), colorTapPosTwoNearPlayer);
            }
            else
            {
                ChangeTapPosColor(GetTapPosInList(1), colorTapPosTwoNearPlayer);
            }
        }
        else
        {
            if (GetTapPosInList(0) == null) return;
            ChangeTapPosColor(GetTapPosInList(0), colorTapPosNearPlayer);
            ChangeTapPosColor(GetTapPosInList(1), colorTapPosNearPlayer);
            ChangeTapPosColor(GetTapPosInList(2), colorTapPosTwoNearPlayer);
        }
    }
    private void ChangeTapPosColor(GameObject tapPos, Color32 color)
    {
        SpriteRenderer rd = tapPos.GetComponent<SpriteRenderer>();
        rd.color = color;
    }
}
