using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum ResourceType
{
    Oxygen,
    Water,
    Food,
    Health
}

public class ResourceManager : MonoBehaviour
{
    [Header("Resource Rates")]
    [SerializeField] private float oxygenDecreaseRate;
    [SerializeField] private float waterDecreaseRate;
    [SerializeField] private float foodDecreaseRate;
    [SerializeField] private float healthDecreaseRate;

    [Header("Resource Caps")]
    [SerializeField] private float maxOxygen;
    [SerializeField] private float maxWater;
    [SerializeField] private float maxFood;
    [SerializeField] private float maxHealth;

    private bool shouldDecrease;

    public static ResourceManager Instance;

    public GameEvent onPlayerDeath;

    public float oxygen;
    public float water;
    public float food;
    public float health;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize resources
        ResetResources();
    }

    private void Update()
    {
        shouldDecrease = !GameObject.FindGameObjectWithTag("Base").GetComponent<BaseEntryController>().playerInsideBase;

        if (shouldDecrease)
        {
            DecreaseResources();
        }
        else
        {
            IncreaseResources();
        }

        CheckPlayerDeath();

        Volume volume = GameObject.FindGameObjectWithTag("Volume").GetComponent<Volume>();
        VolumeProfile profile = volume.sharedProfile;

        profile.TryGet<Vignette>(out var vignette);
        vignette.intensity.value = (10 - Mathf.Min(food, health, water, oxygen)) * 0.5f / 10f;

        profile.TryGet<MotionBlur>(out var motionblur);
        motionblur.intensity.value = (10 - Mathf.Min(food, health, water, oxygen)) / 10f;
    }

    private void CheckPlayerDeath()
    {
        if (oxygen <= 0f || water <= 0f || food <= 0f || health <= 1f)
        {
            onPlayerDeath.Raise();
        }
    }

    private void DecreaseResources()
    {
        DecreaseResource(ref oxygen, oxygenDecreaseRate);
        DecreaseResource(ref water, waterDecreaseRate);
        DecreaseResource(ref food, foodDecreaseRate);
        IncreaseResource(ref health, healthDecreaseRate);

        CheckResourceLimits();
    }

    private void IncreaseResources()
    {
        IncreaseResource(ref oxygen, oxygenDecreaseRate);
        IncreaseResource(ref water, waterDecreaseRate);
        IncreaseResource(ref food, foodDecreaseRate);
        IncreaseResource(ref health, healthDecreaseRate);

        ClampResources();
    }

    private void DecreaseResource(ref float resource, float decreaseRate)
    {
        resource -= decreaseRate * Time.deltaTime;
    }

    private void IncreaseResource(ref float resource, float increaseRate)
    {
        resource += increaseRate * Time.deltaTime;
    }

    private void CheckResourceLimits()
    {
        ClampResource(ref oxygen, maxOxygen);
        ClampResource(ref water, maxWater);
        ClampResource(ref food, maxFood);
        ClampResource(ref health, maxHealth);
    }

    private void ClampResource(ref float resource, float maxAmount)
    {
        resource = Mathf.Clamp(resource, 0f, maxAmount);
    }

    private void ClampResources()
    {
        ClampResource(ref oxygen, maxOxygen);
        ClampResource(ref water, maxWater);
        ClampResource(ref food, maxFood);
        ClampResource(ref health, maxHealth);
    }

    public void ResetResources()
    {
        oxygen = maxOxygen;
        water = maxWater;
        food = maxFood;
        health = maxHealth;
    }

    public void IncreaseResource(ResourceType type, float amount)
    {
        switch (type)
        {
            case ResourceType.Oxygen:
                oxygen = Mathf.Clamp(oxygen + amount, 0f, maxOxygen);
                break;
            case ResourceType.Water:
                water = Mathf.Clamp(water + amount, 0f, maxWater);
                break;
            case ResourceType.Food:
                food = Mathf.Clamp(food + amount, 0f, maxFood);
                break;
            case ResourceType.Health:
                health = Mathf.Clamp(health + amount, 0f, maxHealth);
                break;
        }
    }
    public void DecreaseResource(ResourceType type, float amount)
    {
        switch (type)
        {
            case ResourceType.Oxygen:
                oxygen = Mathf.Clamp(oxygen - amount, 0f, maxOxygen);
                break;
            case ResourceType.Water:
                water = Mathf.Clamp(water - amount, 0f, maxWater);
                break;
            case ResourceType.Food:
                food = Mathf.Clamp(food - amount, 0f, maxFood);
                break;
            case ResourceType.Health:
                health = Mathf.Clamp(health - amount, 0f, maxHealth);
                break;
        }
    }
}

