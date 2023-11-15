using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class TapPosController : BaseBehaviour
{
    public static TapPosController instance;
    private Vector3 startPos;
    private Vector3 endPos;
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
        instance = this;
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        ManageLine();
    }
    private void FixedUpdate()
    {
        Moving();
    }
    public void Moving()
    {
        playerPos = PlayerController.instance.transform.position;
        
        startPos += (Vector3)PlayerController.instance.GetRigid().velocity * Time.fixedDeltaTime;
        endPos += (Vector3)PlayerController.instance.GetRigid().velocity * Time.fixedDeltaTime;

        countDown += Time.fixedDeltaTime * speed;   
        float t = countDown / totalTime;
        if (t < 1)
        {
            Vector3 centerPosition = Vector3.Lerp(startPos, playerPos, 0.5f);
            centerPosition -= new Vector3(0, -sizeAngle, 0);
            currentPos = Vector3.Slerp(startPos - centerPosition, playerPos - centerPosition, t) + centerPosition;
            endTapPos.transform.position = Vector3.Slerp(startPos - centerPosition, playerPos - centerPosition, 0.93f) + centerPosition;
        }
        else
        {
            Vector3 centerPosition = Vector3.Lerp(endPos, playerPos, 0.5f);
            centerPosition -= new Vector3(0, sizeAngle, 0);
            currentPos = Vector3.Slerp(playerPos - centerPosition , endPos - centerPosition, t-1) + centerPosition;
        }
        if (t > 1.2f) SceneManager.LoadScene(0);
        transform.position = currentPos;
    }
    public void SetDoubleTapPos(int side, Color32 color)
    {
        isDoubleTap = true;
        SpriteRenderer rd = GetComponent<SpriteRenderer>();
        rd.color = color;
        SetTapPos(side);
    }
    public void SetTapPos(int side)
    {
        playerPos = PlayerController.instance.transform.position;
        Vector3 newStartPos = new(playerPos.x + (8 * (side == 0 ? -1 : 1)), playerPos.y, 0);
        Vector3 newEndPos = new(playerPos.x + (8 * (side == 0 ? 1 : -1)), playerPos.y, 0);
        transform.position = newStartPos;
        startPos = newStartPos;
        endPos = newEndPos;
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
    public void SetTapPosWhenNearPlayer()
    {
        endTapPos.SetActive(true);
        CreateLine();
        isNearPlayer = true;
    }
    public void CreateLine()
    {
        for(int i = 0; i < 10; i++)
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
            lineArray[i].SetActive(true);
            Vector3 centerPosition = Vector3.Lerp(transform.position, playerPos, 0.5f);      
            centerPosition -= new Vector3(0, -sizeAngle, 0);
            lineArray[i].transform.position = Vector3.Slerp(transform.position - centerPosition, playerPos - centerPosition, linePoint) + centerPosition;
            linePoint += 1f / (lineArray.Length - 1);  // Corrected calculation
        }
    }
    public void SetSizeAngle(float sizeAngle)
    {
        this.sizeAngle = sizeAngle;
    }
}
