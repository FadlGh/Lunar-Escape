using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RocketBar : MonoBehaviour
{
    [SerializeField] private Animator bar;
    [SerializeField] private Slider slider;

    Dictionary<string, int> requiredItems = new Dictionary<string, int>
        {
            { "Moon Rock", 2 },
            { "Energy", 2 },
            { "Metal", 2 }
        };

    void Start()
    {
        slider.value = GameMaster.Instance.sliderValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Upgrade();
        bar.SetBool("Open", true);
    }

    private void Upgrade()
    {
        if (InventoryManager.Instance.CheckItemsAvailability(requiredItems))
        {
            GameMaster.Instance.sliderValue += 20f;
            slider.value = GameMaster.Instance.sliderValue;
            foreach (var kvp in requiredItems)
            {
                InventoryManager.Instance.RemoveItem(kvp.Key, kvp.Value);
            }
            Upgrade();
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bar.SetBool("Open", false);
    }
}
