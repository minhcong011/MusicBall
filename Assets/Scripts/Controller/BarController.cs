using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : BaseBehaviour
{
    private BoxCollider2D boxCollider2D;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void GetComponent()
    {
        base.GetComponent();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        boxCollider2D.enabled = false;
    }
}
