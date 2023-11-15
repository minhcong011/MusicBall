using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : BaseBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D rb;
    [SerializeField] private float maxFallSpeed;
    private bool isCanTap;

    private List<GameObject> listDoubleTap = new();
    private bool isCanDoubleTap;

    private PlayerColorController cl;

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
        CheckFinishTap(TapPosManager.instance.GetTapPosInList(0));
        CheckFinishDoubleTap();
        ManageFallSpeed();
    }
    public override void GetComponent()
    {
        base.GetComponent();
        cl = GetComponent<PlayerColorController>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void ManageFallSpeed()
    {
        float clampedSpeedY = Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed);

        rb.velocity = new Vector3(rb.velocity.x, clampedSpeedY);
    }
    public Vector2 CalculateNextPosition(float time)
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

        Vector3 initialPosition = rb.position;
        float xInitial = initialPosition.x;
        float yInitial = initialPosition.y;
        float vInitial = rb.velocity.magnitude;
        float thetaRad = Mathf.Deg2Rad * angle;

        float xFinal = xInitial + vInitial * time * Mathf.Cos(thetaRad);
        float yFinal = yInitial + vInitial * time * Mathf.Sin(thetaRad) - 0.5f * Physics.gravity.y * time * time;

        return new Vector2(xFinal, yFinal);
    }

    public Rigidbody2D GetRigid()
    {
        return rb;
    }
    public void IncreseScore(GameObject tapPos)
    {
        float score;
        float distanceWithTapPos = Vector2.Distance(tapPos.transform.position, transform.position);
        if (distanceWithTapPos > 1f) score = 10;
        else if (distanceWithTapPos > 0.5f) score = 20;
        else score = 30;
        GameManager.instance.IncreaseScore(score);
    }
    //public GameObject GetNearTapPos()
    //{
    //    GameObject[] tapPos = GameObject.FindGameObjectsWithTag("TapPos");
    //    List<GameObject> objectsToSort = tapPos.ToList();
    //    GameObject nearestObject = objectsToSort.OrderBy(obj =>
    //       Vector3.Distance(transform.position, obj.transform.position)).FirstOrDefault();

    //    if (nearestObject != null)
    //    {
    //        nearestObject.GetComponent<TapPosController>().SetTapPosWhenNearPlayer();
    //        return nearestObject;
    //    }
    //    return null;
    //}
    //public List<GameObject> GetListDoubleTapPos()
    //{
    //    GameObject[] tapPos = GameObject.FindGameObjectsWithTag("TapPos");
    //    List<GameObject> doubleTapPos = new();
    //    foreach (GameObject _tapPos in tapPos)
    //    {
    //        if(_tapPos.GetComponent<TapPosController>().isDoubleTap)
    //        {
    //            doubleTapPos.Add(_tapPos);
    //        }
    //        if (doubleTapPos.Count >= 2) break;
    //    }
    //    if(doubleTapPos.Count == 0) return null;
    //    float compareDistanceWithPlayer = Mathf.Abs(Vector2.Distance(transform.position, doubleTapPos[0].transform.position) - Vector2.Distance(transform.position, doubleTapPos[1].transform.position));
    //    foreach(GameObject _tapPos in doubleTapPos)
    //    {
    //        if (_tapPos == GetNearTapPos() && compareDistanceWithPlayer < 0.1f)
    //        {
    //            foreach (GameObject __tapPos in doubleTapPos)
    //            {
    //                __tapPos.GetComponent<TapPosController>().SetTapPosWhenNearPlayer();
    //            }
    //            return doubleTapPos;
    //        }
    //    }
    //    return null;
    //}
    public void CheckFinishTap(GameObject tapPos)
    {
        //BarManager.instance.SpawnBar(1);
        //Destroy(tapPos);
        //AudioManager.instance.PlayKeySound();
        //IncreseScore(tapPos);
        //LineController lineController = GetComponent<LineController>();
        //return;
        if (!isCanTap) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            FinishTap(-1, tapPos);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            FinishTap(1, tapPos);
        }
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            FinishTap(Camera.main.ScreenToWorldPoint(touch.position).x > transform.position.x ? 1 : -1, tapPos);
        }
    }
    private List<GameObject> listDoubleTapPos = new();
    public void CheckFinishDoubleTap()
    {
        if (!isCanDoubleTap) return;
        listDoubleTapPos.Clear();
        if(Input.GetKeyDown(KeyCode.A)/* && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.S)*/) 
        {
            int side = 1;
            listDoubleTapPos.Add(TapPosManager.instance.GetTapPosInList(0));
            listDoubleTapPos.Add(TapPosManager.instance.GetTapPosInList(1));
            for(int i = 0; i < listDoubleTapPos.Count; i++)
            {
                Debug.Log(listDoubleTap.Count);
                FinishTap(side, listDoubleTapPos[i]);
                side *= -1;
            }
        }
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            int side = 1;
            listDoubleTapPos.Add(TapPosManager.instance.GetTapPosInList(0));
            listDoubleTapPos.Add(TapPosManager.instance.GetTapPosInList(1));
            for (int i = 0; i < listDoubleTapPos.Count; i++)
            {
                Debug.Log(listDoubleTap.Count);
                FinishTap(side, listDoubleTapPos[i]);
                side *= -1;
            }
        }
    }
    private void FinishTap(int barSide, GameObject tapPos)
    {
        Destroy(tapPos);
        TapPosManager.instance.GetListTapPos().Remove(tapPos);
        //AudioManager.instance.PlayKeySound();
        IncreseScore(tapPos);
        isCanDoubleTap = false;
        isCanTap = false;
        cl.ChangeColor(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TapPos"))
        {
            if (collision.gameObject == TapPosManager.instance.GetTapPosInList(0))
            {
                if (collision.gameObject.GetComponent<TapPosController>().isDoubleTap) isCanDoubleTap = true;
                else isCanTap = true;
            }
        }
    }
}

