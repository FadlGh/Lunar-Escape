using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [HideInInspector] public float oxygen;
    [Header("Oxygen")]
    [SerializeField] private float oxygenMax;
    [SerializeField] private float oxygenDecreaseRate;
    [SerializeField] private Slider oxygenSlider;

    [HideInInspector] public float water;
    [Header("Water")]
    [SerializeField] private float waterMax;
    [SerializeField] private float waterDecreaseRate;
    [SerializeField] private Slider waterSlider;

    [HideInInspector] public float food;
    [Header("Food")]
    [SerializeField] private float foodMax;
    [SerializeField] private float foodDecreaseRate;
    [SerializeField] private Slider foodSlider;

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

        oxygenSlider.value = oxygen;
        waterSlider.value = water;
        foodSlider.value = food;

        print(oxygen);
    }

    void Decrease()
    {
        oxygen -= oxygenDecreaseRate * Time.deltaTime;
        water -= waterDecreaseRate * Time.deltaTime;
        food -= foodDecreaseRate * Time.deltaTime;
    }
}
