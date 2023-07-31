using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [HideInInspector] public float oxygen;
    [Header("Oxygen")]
    [SerializeField] private float oxygenMax;
    [SerializeField] private float oxygenDecreaseRate;

    [HideInInspector] public float water;
    [Header("Water")]
    [SerializeField] private float waterMax;
    [SerializeField] private float waterDecreaseRate;

    [HideInInspector] public float food;
    [Header("Food")]
    [SerializeField] private float foodMax;
    [SerializeField] private float foodDecreaseRate;

    private bool shouldDecrease;

    void Start()
    {
        oxygen = oxygenMax;
        water = waterMax;
        food = foodMax;
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
        else if (oxygen < oxygenMax || water < waterMax || food < foodMax)
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
