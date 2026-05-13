using UnityEngine;

public class Game : MonoBehaviour
{
    public UI Ui;
    public GameTimer gameTimer;
    public Music Music;
    
    public void Start()
    {
       Ui.HideGameOverScreen();
       Ui.ShowStartScreen();
       //StartGame();
       Music.PlayMenuMusic();
    }
    public void OnStartButtonClicked()
    { 
        Ui.HideStartScreen();
       StartGame();
    }

    private void StartGame()
    {
        gameTimer.StartTimer(durationInSeconds: 10, OnTimerFinished);
        Ui.ShowTime();
        Music.PlayGameMusic();
    }

    private void OnTimerFinished()
    {
        print("Timer Finished");
        Ui.ShowGameOverScreen();
        Music.PlayMenuMusic();
    }

    public void Update()
    {
        Ui.ShowTime();
    }

    public void OnPlayAgainButtonClicked()
    {
        Ui.HideGameOverScreen();
        StartGame();
    }

}
