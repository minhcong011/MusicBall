using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class TapPosController : BaseBehaviour
{
    private Vector3 startPos;
    private Vector3 playerPos;
    private float countDown;
    [SerializeField] private float totalTime;
    [SerializeField] private float sizeAngle;
    [SerializeField] private float speed;
    [SerializeField] private GameObject endTapPos;
    [SerializeField] private GameObject linePref;
    private GameObject[] lineArray = new GameObject[10];
    private float linePoint;
    private bool isNearPlayer;
    private Vector3 currentPos;

    public bool isDoubleTap;
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        CheckTapPosNearPlayer();
        ManageLine();
    }
    private void FixedUpdate()
    {
        Moving();
    }
    public void Moving()
    {
        countDown += Time.fixedDeltaTime * speed;
        float t = countDown / totalTime;
        playerPos = PlayerController.instance.transform.position;
        currentPos = Vector3.Lerp(startPos, playerPos, t);
        if (t > 1.2f) SceneManager.LoadScene(0);
        transform.position = currentPos;
    }
    public void SetDoubleTapPos(Vector2 spawnPos)
    {
        isDoubleTap = true;
        SpriteRenderer rd = GetComponent<SpriteRenderer>();
        rd.color = new Color32(0, 255, 0, 255);
        SetTapPos(spawnPos);
    }
    public void SetTapPos(Vector2 spawnPos)
    {
        transform.position = spawnPos;
        startPos = spawnPos;
        countDown = 0;
    }
    public void SetIsDoubleTap(bool isDoubleTap)
    {
        this.isDoubleTap = isDoubleTap;
    }
    public bool GetIsDoubleTap()
    {
        return isDoubleTap;
    }
    public void CheckTapPosNearPlayer()
    {
        if (isNearPlayer) return;
        if (isDoubleTap && (gameObject == TapPosManager.instance.GetTapPosInList(0) || gameObject == TapPosManager.instance.GetTapPosInList(1)))
        {
            if (gameObject == TapPosManager.instance.GetTapPosInList(0) && !TapPosManager.instance.GetTapPosInList(1).GetComponent<TapPosController>().isDoubleTap) return;
            if (gameObject == TapPosManager.instance.GetTapPosInList(1) && !TapPosManager.instance.GetTapPosInList(0).GetComponent<TapPosController>().isDoubleTap) return;
            CreateLine();
            isNearPlayer = true;
        }
        else if (TapPosManager.instance.GetTapPosInList(0) == gameObject)
        {
            CreateLine();
            isNearPlayer = true;
        }
    }
    public void CreateLine()
    {
        for (int i = 0; i < 10; i++)
        {
            if (lineArray[i] == null)
                lineArray[i] = Instantiate(linePref);
            lineArray[i].transform.SetParent(transform, false);
        }
    }
    public void ManageLine()
    {
        linePoint = 0;
        if (!isNearPlayer) return;
        for (int i = 0; i < lineArray.Length; i++)
        {
            lineArray[i].transform.position = Vector3.Lerp(transform.position, playerPos, linePoint);
            linePoint += 1f / (lineArray.Length - 1);  // Corrected calculation
        }
    }
}
