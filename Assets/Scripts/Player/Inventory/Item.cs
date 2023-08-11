using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(itemName, amount);
            GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>().Play("Collect");
            Destroy(gameObject);
        }
    }
}
