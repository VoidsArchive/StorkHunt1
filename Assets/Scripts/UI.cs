using TMPro;
using UnityEngine;

public class UI : MonoBehaviour

{
    //public TMP_Text scoreText;
    public TMP_Text timeText;
    public CanvasGroup StartScreenCanvasGroup;
    public CanvasGroup GameOverScreenCanvasGroup;
    public GameObject GamePlayBackground;
    public GameTimer GameTimer;
    public TMP_Text bulletText;

    public void SetScoreText(int score)
    {
        //scoreText.text = "Score: " + score;
    }

    public void ShowStartScreen()
    {
       CanvasGroupDisplayer.Show(StartScreenCanvasGroup);
    }
    
    public void HideStartScreen()
    {
        CanvasGroupDisplayer.Hide(StartScreenCanvasGroup);
    }
    
    public void HideGameOverScreen()
    {
        CanvasGroupDisplayer.Hide(GameOverScreenCanvasGroup);
    }
    
    public void ShowGameOverScreen()
    {
        CanvasGroupDisplayer.Show(GameOverScreenCanvasGroup);
    }
    
    
    
   
    

    public void ShowTime()
    {
        timeText.text = GameTimer.GetTimeAsString();
        if (GameTimer.GetSecondsRemaining() == 3)
        {
            timeText.color = Color.red;
        }
    }
}