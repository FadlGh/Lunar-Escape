using UnityEngine;
using TMPro;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moonRockText;
    [SerializeField] private TMP_Text metalText;
    [SerializeField] private TMP_Text energyText;

    void Update()
    {
        moonRockText.text = InventoryManager.Instance.GetQuantity("Moon Rock").ToString();
        metalText.text = InventoryManager.Instance.GetQuantity("Metal").ToString();
        energyText.text = InventoryManager.Instance.GetQuantity("Energy").ToString();
    }
}
