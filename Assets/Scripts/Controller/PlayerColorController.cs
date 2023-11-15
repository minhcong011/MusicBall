using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerColorController : BaseBehaviour
{
    [SerializeField] private Color32[] color;
    private Light2D light2D;
    public override void Update()
    {
        base.Update();
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.GetGameStage() != GameManager.GameStage.PLaying) return;
        ManageLight();
    }
    public override void GetComponent()
    {
        base.GetComponent();
        light2D = GetComponentInChildren<Light2D>();
    }
    public void ChangeColor(GameObject obj)
    {
        int randColor = Random.Range(0, color.Length);
        SpriteRenderer rd = obj.GetComponent<SpriteRenderer>();
        rd.color = color[randColor];
        light2D.color = color[randColor];
    }
    private void ManageLight()
    {       
        light2D.intensity = Mathf.Clamp(AudioManager.instance.GetAudioLevel() * 1000 - 0.5f, 0.5f, 1.2f);
    }
}
