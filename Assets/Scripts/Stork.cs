using UnityEngine;

public class Stork : TimedObject
{ 
    private GameObject powerUpPrefab;
    private float powerUpDropChance = GameParameters.PowerUpDropChance;
    private bool wasShotDown;
    private ObjectMover objectMover;
    private float originalSpeed;
    private static float GlobalSpeedMultiplier = 1f;
    private static float GlobalSpeedExpiry = 0f;
    
    public Animator animator;
    public Sounds Sounds;
    
    public new void Start()
    {
        secondsOnScreen = GameParameters.StorkSecondsOnScreen;
        objectMover = GetComponent<ObjectMover>();
        if (objectMover != null)
        {
            originalSpeed = objectMover.speed;
            objectMover.speed = originalSpeed * GlobalSpeedMultiplier;
        }
        base.Start();
    }
    private void Update()
    {
        if (!Game.isGameActive)
        {
            Destroy(gameObject);
        }
        if (GlobalSpeedMultiplier != 1f && Time.time > GlobalSpeedExpiry)
        {
            ResetGlobalSpeedMultiplier();
        }
    }
    public void MarkAsShotDown()
    {
        wasShotDown = true;
        StartFalling();
        
    }
    public bool IsShotDown
    {
        get { return wasShotDown; }
    }
    
    public void StartFalling()
    {
        animator.SetBool("IsFalling", true);
        Sounds.PlayFallingSound();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer") || other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (!Application.isPlaying || !wasShotDown || powerUpPrefab == null)
        {
            return;
        }
        if (Random.value > powerUpDropChance)
        {
            return;
        }
        GameObject spawnedPickup = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        PowerUps pickup = spawnedPickup.GetComponent<PowerUps>();
        if (pickup != null)
        {
            pickup.RollRandomEffect();
        }
    }
    public static void ApplyGlobalSpeedMultiplier(float multiplier, float durationInSeconds)
    {
        GlobalSpeedMultiplier = multiplier;
        GlobalSpeedExpiry = Time.time + durationInSeconds;
        Stork[] all = FindObjectsOfType<Stork>();
        foreach (var s in all)
        {
            if (s.objectMover != null)
            {
                s.objectMover.speed = s.originalSpeed * GlobalSpeedMultiplier;
            }
        }
    }
    private static void ResetGlobalSpeedMultiplier()
    {
        Stork[] all = FindObjectsOfType<Stork>();
        foreach (var s in all)
        {
            if (s.objectMover != null)
            {
                s.objectMover.speed = s.originalSpeed;
            }
        }
        GlobalSpeedMultiplier = 1f;
        GlobalSpeedExpiry = 0f;
    }
}