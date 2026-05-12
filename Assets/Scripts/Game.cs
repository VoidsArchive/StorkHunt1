using UnityEngine;

public class Game : MonoBehaviour
{
    public UI Ui;
    public GameClock gameClock;
    
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
        gameClock.StartTimer(OnTimerFinished);
        Ui.ShowTime();
        Ui.ShowActivePowerups();
    }

    private void OnTimerFinished()
    {
        print("Timer Finished");
        //Ui.ShowGameOverScreen();
    }

    public void Update()
    {
        Ui.ShowTime();
        Ui.ShowActivePowerups();
    }

    public void OnPlayAgainButtonClicked()
    {
        Ui.HideGameOverScreen();
        StartGame();
    }

}
