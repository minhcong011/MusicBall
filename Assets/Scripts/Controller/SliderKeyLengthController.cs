using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderKeyLengthController : BaseBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetSlider(1);
    }

    public override void Update()
    {
        base.Update();
        ManageSlider();
    }
    public override void GetComponent()
    {
        base.GetComponent();
        slider = GetComponent<Slider>();
    }
    public void SetSlider(float maxValue)
    {
        slider.maxValue = maxValue;
    }
    private void ManageSlider()
    {
        slider.value += Time.deltaTime;
        Vector2 start = gameObject.transform.GetChild(1).transform.position;
        Vector2 end = gameObject.transform.GetChild(0).transform.position;
        if(Mathf.Abs(start.y - end.y) < 1)
        {
            
        }
    }
}
