using TMPro;
using UnityEngine;

public class UI : MonoBehaviour

{
    //public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text activePowerupText;
    //public CanvasGroup StartScreenCanvasGroup;
    //public CanvasGroup GameOverScreenCanvasGroup;
    public GameClock gameClock;

    private void Awake()
    {
        if (activePowerupText == null)
        {
            Transform powerupTextTransform = transform.Find("Text (TMP) - Powerups");
            if (powerupTextTransform != null)
            {
                activePowerupText = powerupTextTransform.GetComponent<TMP_Text>();
            }
        }
    }

    public void SetScoreText(int score)
    {
        //scoreText.text = "Score: " + score;
    }

    public void ShowStartScreen()
    {
       // CanvasGroupDisplayer.Show(StartScreenCanvasGroup);
    }
    public void HideStartScreen()
    {
        //CanvasGroupDisplayer.Hide(StartScreenCanvasGroup);
    }
    
    public void HideGameOverScreen()
    {
        //CanvasGroupDisplayer.Hide(GameOverScreenCanvasGroup);
    }
    
    public void ShowGameOverScreen()
    {
        //CanvasGroupDisplayer.Show(GameOverScreenCanvasGroup);
    }
    

    public void ShowTime()
    {
        if (timeText != null && gameClock != null)
        {
            timeText.text = gameClock.GetTimeAsString();
        }
    }

    public void ShowActivePowerups()
    {
        if (activePowerupText != null)
        {
            activePowerupText.text = PowerUps.GetActiveTimedPowerupsText();
        }
    }
}