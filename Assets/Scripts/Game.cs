using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    public static Game Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public UI Ui;
    public GameTimer gameTimer;
    public PlayerHealth PlayerHealth;
    public Music Music;
    public Sounds Sounds;
    public static bool isGameActive = false;
    public Mongoose mongoosePrefab;
    private Mongoose activeMongoose;
    public StorkSpawner storkSpawnerPrefab;
    private StorkSpawner activeStorkSpawner;
    
    public void Start()
    {
        if (Ui != null)
        {
            Ui.HideGameOverScreen();
            Ui.HideGameplayScreen();
            Ui.ShowStartScreen();
        }
        else
        {
            Debug.LogWarning("Game: Ui reference is null in Start");
        }

        if (Music != null)
        {
           Music.PlayMenuMusic();
        }
        else
        {
            Debug.LogWarning("Game: Music reference is null in Start");
        }
        activeMongoose = FindObjectOfType<Mongoose>();
        activeStorkSpawner = FindObjectOfType<StorkSpawner>();
    }
    public void OnStartButtonClicked()
    { 
        Debug.Log("Game: OnStartButtonClicked invoked");
        SetIsGameActive(true);
        if (Ui != null)
        {
            Ui.HideStartScreen();
            Sounds.PlayClickSound();
        }
        else
            Debug.LogWarning("Game: Ui is null in OnStartButtonClicked");

       StartGame();
    }

    private void StartGame()
    {
        Debug.Log("Game: StartGame called");
        Shooter.ResetStorksShot();
        gameTimer.StartTimer();
        Ui.HideGameOverScreen();
        Ui.ShowGameplayScreen();
        Ui.ShowTime();
        Ui.ShowActivePowerups();
        Music.PlayGameMusic();
    }

    private void GameOver()
    {
        if (!GetIsGameActive())
            return;

        SetIsGameActive(false);
        Ui.HideGameplayScreen();
        Ui.ShowGameOverScreen();
        Music.PlayMenuMusic();
        PowerUps.ClearActiveTimedPowerups();
        
        if (gameTimer != null)
            gameTimer.StopTimer();
        if (activeMongoose != null)
        {
            Destroy(activeMongoose.gameObject);
            activeMongoose = null;
        }

        if (activeStorkSpawner != null)
        {
            Destroy(activeStorkSpawner.gameObject);
            activeStorkSpawner = null;
        }
        Stork[] storks = FindObjectsOfType<Stork>();
        foreach (var st in storks)
        {
            Destroy(st.gameObject);
        }
        PowerUps[] pickups = FindObjectsOfType<PowerUps>();
        foreach (var p in pickups)
        {
            Destroy(p.gameObject);
        }
        DamagingProjectiles[] projectiles = FindObjectsOfType<DamagingProjectiles>();
        foreach (var proj in projectiles)
        {
            Destroy(proj.gameObject);
        }
    }

    public void Update()
    {
        if (PlayerHealth != null)
        {
            if (PlayerHealth.GetPlayerHealth() <= 0)
            {
                GameOver();
            }
        }

        if (Ui != null)
        {
            Ui.ShowTime();
            Ui.ShowHealth();
            Ui.ShowActivePowerups();
        }
    }

    public void OnPlayAgainButtonClicked()
    {
        Debug.Log("Game: OnPlayAgainButtonClicked invoked");
        Ui.HideGameOverScreen();
        Sounds.PlayClickSound();
        SetIsGameActive(true);
        PlayerHealth.ResetHealth();
        Ui.ShowHealth();
        gameTimer.ResetTimer();
        Music.PlayGameMusic();
        if (activeMongoose == null)
        {
            GameObject go = Instantiate(mongoosePrefab.gameObject);
            activeMongoose = go.GetComponent<Mongoose>();
        }

        if (activeStorkSpawner == null)
        {
            GameObject go = Instantiate(storkSpawnerPrefab.gameObject);
            activeStorkSpawner = go.GetComponent<StorkSpawner>();
        }

        Debug.Log("Game: Restart complete - calling StartGame");
        StartGame();
    }
    public static bool GetIsGameActive()
    {
        return isGameActive;
    }

    public static void SetIsGameActive(bool isActive)
    {
        isGameActive = isActive;
    }
}
