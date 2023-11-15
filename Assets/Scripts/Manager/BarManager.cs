using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : BaseBehaviour
{
    public static BarManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject barPref;

    private bool isCanCreateBar;
    private GameObject sliderLengthKey;
    // Start is called before the first frame update
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
        if (GameManager.instance.GetGameStage() != GameManager.GameStage.PLaying) return;
    }
    private void ManageSpawnBar()
    {
        if (Input.touchCount == 0 || !isCanCreateBar) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            if (Camera.main.ScreenToWorldPoint(touch.position).x > player.transform.position.x)
            {
                SpawnBar(1);
            }
            else
            {
                SpawnBar(-1);
            }
        }
        isCanCreateBar = false;
    }
    public void SpawnBar(int side)
    {
        GameObject newBar = Instantiate(barPref);
        Vector3 newPos;
        if (side  == 1) 
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                newBar.transform.rotation = Quaternion.Euler(0, 0, -45);
                newPos = player.transform.position + new Vector3(-0.25f,-0.5f);
            }
            else
            {
                newPos = player.transform.position + new Vector3(0.25f, -0.5f);
            }
        }
        else
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                newBar.transform.rotation = Quaternion.Euler(0, 0, 45);
                newPos = player.transform.position + new Vector3(0.5f, -1);
            }
            else
            {
                newPos = player.transform.position + new Vector3(-0.5f, -1);
            }
        }
        newBar.transform.position = newPos;
    }
    public void SetIsCanCreateBar(bool isCanCreateBar)
    {
        this.isCanCreateBar = isCanCreateBar;
    }
}
