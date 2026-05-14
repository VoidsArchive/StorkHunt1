using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum EffectType
    {
        IncreaseHealth,
        IncreaseTime,
        DecreaseHealth,
        DecreaseTime,
        SlowStorkSpeed,
        FastStorkSpeed,
        StorkSwarm
    }

    private sealed class ActiveTimedEffect
    {
        public EffectType effectType;
        public string displayName;
        public float expiryTime;
    }

    private static readonly List<ActiveTimedEffect> ActiveTimedEffects = new List<ActiveTimedEffect>();

    public static void RegisterTimedPowerup(EffectType effectType, string displayName, float durationSeconds)
    {
        if (durationSeconds <= 0f)
        {
            return;
        }

        float expiryTime = Time.time + durationSeconds;
        ActiveTimedEffect existing = ActiveTimedEffects.Find(effect => effect.effectType == effectType);
        if (existing != null)
        {
            existing.displayName = displayName;
            existing.expiryTime = expiryTime;
            return;
        }

        ActiveTimedEffects.Add(new ActiveTimedEffect
        {
            effectType = effectType,
            displayName = displayName,
            expiryTime = expiryTime
        });
    }

    public static string GetActiveTimedPowerupsText()
    {
        CleanupExpiredTimedPowerups();

        if (ActiveTimedEffects.Count == 0)
        {
            return "Active Powerups:\nNone";
        }

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Active Powerups:");
        for (int i = 0; i < ActiveTimedEffects.Count; i++)
        {
            ActiveTimedEffect effect = ActiveTimedEffects[i];
            int secondsRemaining = Mathf.CeilToInt(effect.expiryTime - Time.time);
            if (secondsRemaining < 0)
            {
                secondsRemaining = 0;
            }

            builder.Append(effect.displayName);
            builder.Append(" (");
            builder.Append(secondsRemaining);
            builder.AppendLine("s)");
        }

        return builder.ToString().TrimEnd();
    }

    private static void CleanupExpiredTimedPowerups()
    {
        ActiveTimedEffects.RemoveAll(effect => effect.expiryTime <= Time.time);
    }

    public static void ClearActiveTimedPowerups()
    {
        ActiveTimedEffects.Clear();
        Debug.Log("PowerUps: Cleared all active timed powerups");
    }

    [Header("Buff / Debuff Roll")] [Range(0f, 1f)] [SerializeField]
    private float buffChance = 0.5f;

    [SerializeField] private EffectType[] availableBuffs = { EffectType.IncreaseHealth };

    [SerializeField] private EffectType[] availableDebuffs =
        { EffectType.DecreaseHealth, EffectType.DecreaseTime, EffectType.FastStorkSpeed, EffectType.StorkSwarm };

    [Header("Health Effect Values")] [SerializeField]
    private int minHealthIncrease = 1;

    [SerializeField] private int maxHealthIncrease = 3;

    [Header("Time Effect Values")] [SerializeField]
    private int minTimeIncrease = 5;

    [SerializeField] private int maxTimeIncrease = 15;
    [SerializeField] private int minTimeDecrease = 5;
    [SerializeField] private int maxTimeDecrease = 15;

    [Header("Stork Speed Effect")] [SerializeField]
    private float storkSpeedMultiplier = 0.5f; // 50% speed

    [SerializeField] private float storkSpeedDuration = 10f;
    [SerializeField] private float fastStorkSpeedMultiplier = 1.5f;
    [SerializeField] private float fastStorkSpeedDuration = 10f;

    [Header("Pickup Lifetime")] [SerializeField]
    private float lifetimeSeconds = 10f;

    private EffectType rolledEffect;
    private bool hasRolledEffect;

    private void Start()
    {
        if (!hasRolledEffect)
        {
            RollRandomEffect();
        }

        Destroy(gameObject, lifetimeSeconds);
    }

    public void RollRandomEffect()
    {
        bool chooseBuff = Random.value <= buffChance;
        rolledEffect = chooseBuff
            ? ChooseRandomEffectFromPool(availableBuffs, availableDebuffs, EffectType.IncreaseHealth)
            : ChooseRandomEffectFromPool(availableDebuffs, availableBuffs, EffectType.DecreaseHealth);
        hasRolledEffect = true;
    }

    private static EffectType ChooseRandomEffectFromPool(
        EffectType[] preferredPool,
        EffectType[] fallbackPool,
        EffectType fallbackDefault)
    {
        if (preferredPool != null && preferredPool.Length > 0)
        {
            return preferredPool[Random.Range(0, preferredPool.Length)];
        }

        if (fallbackPool != null && fallbackPool.Length > 0)
        {
            return fallbackPool[Random.Range(0, fallbackPool.Length)];
        }

        return fallbackDefault;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        ApplyEffect();
        Destroy(gameObject);
    }

    private void ApplyEffect()
    {
        if (!hasRolledEffect)
        {
            RollRandomEffect();
        }

        switch (rolledEffect)
        {
            case EffectType.IncreaseHealth:
                PlayerHealth.ChangeHealth(Random.Range(minHealthIncrease, maxHealthIncrease + 1));
                break;
            case EffectType.IncreaseTime:
                GameTimer gc = FindObjectOfType<GameTimer>();
                if (gc != null)
                {
                    int add = Random.Range(minTimeIncrease, maxTimeIncrease + 1);
                    gc.AddSeconds(add);
                }
                else
                {
                    Debug.LogWarning("No GameTimer found in scene to apply TimeBoost");
                }

                break;
            case EffectType.DecreaseTime:
                GameTimer gameTimer = FindObjectOfType<GameTimer>();
                if (gameTimer != null)
                {
                    int subtract = Random.Range(minTimeDecrease, maxTimeDecrease + 1);
                    gameTimer.RemoveSeconds(subtract);
                }
                else
                {
                    Debug.LogWarning("No GameTimer found in scene to apply DecreaseTime");
                }

                break;
            case EffectType.SlowStorkSpeed:
                Stork.ApplyGlobalSpeedMultiplier(storkSpeedMultiplier, storkSpeedDuration);
                RegisterTimedPowerup(EffectType.SlowStorkSpeed, "Slow Stork Speed", storkSpeedDuration);
                break;
            case EffectType.FastStorkSpeed:
                Stork.ApplyGlobalSpeedMultiplier(fastStorkSpeedMultiplier, fastStorkSpeedDuration);
                RegisterTimedPowerup(EffectType.FastStorkSpeed, "Fast Stork Speed", fastStorkSpeedDuration);
                break;
            case EffectType.StorkSwarm:
                StorkSpawner storkSpawner = FindObjectOfType<StorkSpawner>();
                if (storkSpawner != null)
                {
                    storkSpawner.TriggerStorkSwarm();
                }
                else
                {
                    Debug.LogWarning("No StorkSpawner found in scene to apply StorkSwarm");
                }

                break;
            case EffectType.DecreaseHealth:
                PlayerHealth.ChangeHealth(-1);
                break;
        }
    }
}