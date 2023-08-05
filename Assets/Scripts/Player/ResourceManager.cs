using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [HideInInspector] public float oxygen;
    [Header("Oxygen")]
    [SerializeField] private float oxygenDecreaseRate;

    [HideInInspector] public float water;
    [Header("Water")]
    [SerializeField] private float waterDecreaseRate;

    [HideInInspector] public float food;
    [Header("Food")]
    [SerializeField] private float foodDecreaseRate;

    private bool shouldDecrease;

    public static ResourceManager Instance;

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

    void Start()
    {
        oxygen = 10f;
        water = 10f;
        food = 10f;
    }

    void Update()
    {
        shouldDecrease = !GameObject.FindGameObjectWithTag("Base").GetComponent<BaseEntryController>().playerInsideBase;

        if (oxygen < 0 || water < 0 || food < 0)
        {
            // die
        }

        if (shouldDecrease)
        {
            Decrease();
        }
        else if (oxygen < 10f || water < 10f || food < 10f)
        {
            Increase();
        }
    }

    void Decrease()
    {
        oxygen -= oxygenDecreaseRate * Time.deltaTime;
        water -= waterDecreaseRate * Time.deltaTime;
        food -= foodDecreaseRate * Time.deltaTime;
    }

    void Increase()
    {
        oxygen += oxygenDecreaseRate * Time.deltaTime;
        water += waterDecreaseRate * Time.deltaTime;
        food += foodDecreaseRate * Time.deltaTime;
    }
}
