using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageGamePlayUI();
    }
    private void ManageGamePlayUI()
    {
        currentScoreText.text ="SCORE: " + ((int)GameManager.instance.GetCurrentScore()).ToString();
        bestScoreText.text ="Max score: " + GameCache.GetBestScore(SongData.instance.GetCurrentSongID()).ToString(); 
    }
}
