using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    public virtual void Awake()
    {
        GetComponent();
    }
    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    public virtual void GetComponent()
    {

    }
}
