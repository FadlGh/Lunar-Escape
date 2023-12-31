using UnityEngine;
using UnityEngine.UI;

public class BarsManager : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private Slider waterSlider;
    [SerializeField] private Slider foodSlider;
    [SerializeField] private Slider healthSlider;

    private ResourceManager manager;
    void Start()
    {
        manager = ResourceManager.Instance;
    }

    void Update()
    {
        oxygenSlider.value = manager.oxygen;
        waterSlider.value = manager.water;
        foodSlider.value = manager.food;
        healthSlider.value = manager.health;
    }
}
