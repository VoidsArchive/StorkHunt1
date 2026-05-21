using System.Text;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour

{
    public TMP_Text timeText;
    public TMP_Text healthText;
    public PlayerHealth playerHealth;
    public TMP_Text activePowerupText;
    public TMP_Text statsText;
    public CanvasGroup StartScreenCanvasGroup;
    public CanvasGroup GameOverScreenCanvasGroup;
    public CanvasGroup GameplayScreenCanvasGroup;
    public GameTimer gameTimer;
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
        if (StartScreenCanvasGroup == null)
        {
            CanvasGroup[] all = FindObjectsOfType<CanvasGroup>(true);
            foreach (var cg in all)
            {
                if (cg.gameObject.name.ToLower().Contains("start"))
                {
                    StartScreenCanvasGroup = cg;
                    Debug.Log("UI: Found StartScreenCanvasGroup by name: " + cg.gameObject.name);
                    break;
                }
            }
        }
        if (GameOverScreenCanvasGroup == null)
        {
            CanvasGroup[] all = FindObjectsOfType<CanvasGroup>(true);
            foreach (var cg in all)
            {
                string n = cg.gameObject.name.ToLower();
                if (n.Contains("gameover") || n.Contains("game over") || n.Contains("game_over") || n.Contains("over"))
                {
                    GameOverScreenCanvasGroup = cg;
                    Debug.Log("UI: Found GameOverScreenCanvasGroup by name: " + cg.gameObject.name);
                    break;
                }
            }
        }
        if (StartScreenCanvasGroup != null)
        {
            Button[] startButtons = StartScreenCanvasGroup.GetComponentsInChildren<Button>(true);
            foreach (var b in startButtons)
            {
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(OnStartButtonPressed);
            }
            if (startButtons.Length > 0)
                Debug.Log("UI: Wired StartScreen buttons to OnStartButtonPressed");
        }
        if (GameOverScreenCanvasGroup != null)
        {
            Button[] gameOverButtons = GameOverScreenCanvasGroup.GetComponentsInChildren<Button>(true);
            foreach (var b in gameOverButtons)
            {
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(OnPlayAgainButtonPressed);
            }
            if (gameOverButtons.Length > 0)
                Debug.Log("UI: Wired GameOver buttons to OnPlayAgainButtonPressed");
        }
        Button[] allButtons = FindObjectsOfType<Button>(true);
        if ((StartScreenCanvasGroup == null || StartScreenCanvasGroup.GetComponentsInChildren<Button>(true).Length == 0) ||
            (GameOverScreenCanvasGroup == null || GameOverScreenCanvasGroup.GetComponentsInChildren<Button>(true).Length == 0))
        {
            Debug.Log("UI: Falling back to scanning all Buttons in scene for Play/Start/Again labels");
            foreach (var b in allButtons)
            {
                string n = b.gameObject.name.ToLower();
                bool wired = false;

                if (n.Contains("start") || n.Contains("play") )
                {
                    b.onClick.RemoveAllListeners();
                    b.onClick.AddListener(OnStartButtonPressed);
                    wired = true;
                }
                if (n.Contains("again") || n.Contains("play") || n.Contains("restart"))
                {
                    b.onClick.RemoveAllListeners();
                    b.onClick.AddListener(OnPlayAgainButtonPressed);
                    wired = true;
                }
                if (!wired)
                {
                    TMP_Text label = b.GetComponentInChildren<TMP_Text>(true);
                    if (label != null)
                    {
                        string t = label.text.ToLower();
                        if (t.Contains("play") || t.Contains("start"))
                        {
                            b.onClick.RemoveAllListeners();
                            b.onClick.AddListener(OnStartButtonPressed);
                            wired = true;
                        }
                        if (t.Contains("again") || t.Contains("restart") )
                        {
                            b.onClick.RemoveAllListeners();
                            b.onClick.AddListener(OnPlayAgainButtonPressed);
                            wired = true;
                        }
                    }
                }
                if (wired)
                    Debug.Log($"UI: Wired button '{b.gameObject.name}' based on heuristic");
            }
        }
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
        ShowStats();
        CanvasGroupDisplayer.Show(GameOverScreenCanvasGroup);
    }
    public void ShowGameplayScreen()
    {
        CanvasGroupDisplayer.Show(GameplayScreenCanvasGroup);
    }
    public void HideGameplayScreen()
    {
        CanvasGroupDisplayer.Hide(GameplayScreenCanvasGroup);
    }
    public void ShowTime()
    {
        if (timeText != null && gameTimer != null)
        {
            timeText.text = gameTimer.GetTimeAsString();
        }
    }
    public void ShowActivePowerups()
    {
        if (activePowerupText != null)
        {
            activePowerupText.text = PowerUps.GetActiveTimedPowerupsText();
        }
    }
    public void ShowHealth()
    {
        if (healthText != null)
        {
            healthText.text = playerHealth.HealthAsString();
            healthText.color = playerHealth.GetPlayerHealth() <= 2 ? Color.red : Color.white;

        }
    }
    public string GetStatsText()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Stats:");
        builder.AppendLine("Time Played: " + gameTimer.GetTimeAsString());
        builder.AppendLine("Storks Shot: " + Shooter.getStorksShot());

        return builder.ToString().TrimEnd();
    }
    public void ShowStats()
    {
        if (statsText != null)
        {
            statsText.text = GetStatsText();
        }
    }
    public void Start()
    {
        Button[] allButtons = FindObjectsOfType<Button>(true);
        Debug.Log($"UI: Start() - found {allButtons.Length} Button(s) in scene");
        foreach (var b in allButtons)
        {
            Debug.Log($"UI: Button present: {b.gameObject.name}");
        }
    }
    public void OnStartButtonPressed()
    {
        Debug.Log("UI: OnStartButtonPressed invoked");
        var game = Game.Instance ?? FindObjectOfType<Game>();
        if (game != null)
        {
            game.OnStartButtonClicked();
        }
        else
        {
            Debug.LogWarning("UI: No Game instance found when Start button pressed");
        }
    }
    public void OnPlayAgainButtonPressed()
    {
        Debug.Log("UI: OnPlayAgainButtonPressed invoked");
        var game = Game.Instance ?? FindObjectOfType<Game>();
        if (game != null)
        {
            game.OnPlayAgainButtonClicked();
        }
        else
        {
            Debug.LogWarning("UI: No Game instance found when PlayAgain button pressed");
        }
    }
}