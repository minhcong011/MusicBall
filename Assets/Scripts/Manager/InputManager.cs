using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
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
        
    }
    public bool CheckTouchBegan()
    {
        if (Input.touchCount == 0) return false;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            return true;
        }
        return false;
    }
    public Touch GetTouch()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            return touch;
        }
        touch = new Touch();
        return touch;
    }
}
