using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPosManager : MonoBehaviour
{
    public static TapPosManager instance;
    [SerializeField] private GameObject tapPosPref;
    [SerializeField] private Color32 colorTapPosNearPlayer;
    private int currentKey;
    private float countDown;
    private float length = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetGameStage() != GameManager.GameStage.PLaying) return;
        ManageSpawnTapPos();
        ManageTapPosColor();
    }
    private void ManageSpawnTapPos()
    {
        countDown += Time.deltaTime;
        if (countDown >= length)
        {
            int randSide = Random.Range(0, 2);
            float randSizeAngle = Random.Range(0, 10);
            if (!SongData.instance.GetCurrentSong().keyArray[currentKey].isDoubleNote)
            {
                SpawnTapPos(SongData.instance.GetCurrentSong().keyArray[currentKey].permanentSide ? SongData.instance.GetCurrentSong().keyArray[currentKey].side : randSide, randSizeAngle);
            }
            else
            {
                SpawnTapPos(randSide, new Color32(0,255,0,255),randSizeAngle);
                SpawnTapPos(randSide == 0 ? 1 : 0, new Color32(0, 255, 0, 255), randSizeAngle);
            }
            countDown = 0;
            length = SongData.instance.GetCurrentSong().keyArray[currentKey].length;
            currentKey++;
        }
        if (currentKey >= SongData.instance.GetCurrentSong().keyArray.Length) currentKey = 0;
    }
    private void SpawnTapPos(int side, float sizeAngle)
    {
        GameObject newTapPos = Instantiate(tapPosPref);
        newTapPos.GetComponent<TapPosController>().SetTapPos(side);
        newTapPos.GetComponent<TapPosController>().SetSizeAngle(sizeAngle);
    }
    private void SpawnTapPos(int side, Color32 color, float sizeAngle)
    {
        GameObject newTapPos = Instantiate(tapPosPref);
        newTapPos.GetComponent<TapPosController>().SetSizeAngle(sizeAngle);
        newTapPos.GetComponent<TapPosController>().SetDoubleTapPos(side, color);
    }
    private void ManageTapPosColor()
    {
        if (PlayerController.instance.GetNearTapPos() == null) return;
        if (!PlayerController.instance.GetNearTapPos().GetComponent<TapPosController>().isDoubleTap)
        {
            GameObject nearTapPos = PlayerController.instance.GetNearTapPos();
            ChangeTapPosColor(nearTapPos, colorTapPosNearPlayer);
        }
        {
           if (PlayerController.instance.GetListDoubleTapPos() == null) return;
           foreach(GameObject tapPos in PlayerController.instance.GetListDoubleTapPos())
           {
                ChangeTapPosColor(tapPos, colorTapPosNearPlayer);
           }
        }
    }
    private void ChangeTapPosColor(GameObject tapPos, Color32 color)
    {
        SpriteRenderer rd = tapPos.GetComponent<SpriteRenderer>();
        rd.color = color;
    }
}
