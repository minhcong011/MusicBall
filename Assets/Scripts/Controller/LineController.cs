using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private GameObject linePref;
    [SerializeField] private int amountLinePos;
    private PlayerController playerController;
    private GameObject[] lineArray1;
    private GameObject[] lineArray2;
    private GameObject[] tapPos;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        CreateLineArray(amountLinePos);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.GetListDoubleTapPos() == null || playerController.GetNearTapPos() == null)
        {
            foreach (GameObject linePoint in lineArray1)
            {
                linePoint.SetActive(false);
            }
            foreach (GameObject linePoint in lineArray2)
            {
                linePoint.SetActive(false);
            }
        }
        ManageLine();
    }
    private void FixedUpdate()
    {
    }
    public void SetActiveLineArray()
    {
        foreach(GameObject linePoint in lineArray2)
        {
            linePoint.SetActive(false);
        }
    }
    private void CreateLineArray(int amount)
    {
        lineArray1 = new GameObject[amount];
        lineArray2 = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            lineArray1[i] = Instantiate(linePref);
        }
        for (int i = 0; i < amount; i++)
        {
            lineArray2[i] = Instantiate(linePref);
        }
    } 
    private void DrawLine(GameObject objToDraw, int orderLine)
    {
        float linePoint = 0f;
        if (orderLine == 1)
        {
            for (int i = 0; i < lineArray1.Length; i++)
            {
                lineArray1[i].SetActive(true);
                lineArray1[i].transform.position = Vector3.Lerp(transform.position, objToDraw.transform.position, linePoint);
                linePoint += 1f / (lineArray1.Length - 1);  // Corrected calculation
            }
        }
        else
        {
            for (int i = 0; i < lineArray2.Length; i++)
            {
                lineArray2[i].SetActive(true);
                lineArray2[i].transform.position = Vector3.Lerp(transform.position, objToDraw.transform.position, linePoint);
                linePoint += 1f / (lineArray2.Length - 1);  // Corrected calculation
            }
        }
    }
    private void ManageLine()
    {
        if (playerController.GetNearTapPos() == null) return;       
        if (!playerController.GetNearTapPos().GetComponent<TapPosController>().isDoubleTap)
        {
            DrawLine(playerController.GetNearTapPos(), 1);
        }
        else
        {
            if (playerController.GetListDoubleTapPos() == null) return;
            DrawLine(playerController.GetListDoubleTapPos()[0], 1);
            DrawLine(playerController.GetListDoubleTapPos()[1], 2);
        }      
    }
}
