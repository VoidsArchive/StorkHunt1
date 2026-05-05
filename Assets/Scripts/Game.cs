using UnityEngine;

public class Game : MonoBehaviour
{
    public UI Ui;
    public GameTimer gameTimer;
    
    public void Start()
    {
       // Ui.HideGameOverScreen();
       // Ui.ShowStartScreen();
       StartGame();
    }
    public void OnStartButtonClicked()
    {
        //Ui.HideStartScreen();
       // StartGame();
    }

    private void StartGame()
    {
        gameTimer.StartTimer(durationInSeconds: 10, OnTimerFinished);
        Ui.ShowTime();
    }

    private void OnTimerFinished()
    {
        print("Timer Finished");
        //Ui.ShowGameOverScreen();
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
