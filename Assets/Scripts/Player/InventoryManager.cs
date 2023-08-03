using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventoryItem> inventory = new List<InventoryItem>();

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

    public void AddItem(string itemName, int amount)
    {
        InventoryItem item = inventory.Find(i => i.itemName == itemName);
        if (item != null)
        {
            item.quantity += amount;
        }
        else
        {
            InventoryItem newItem = new InventoryItem { itemName = itemName, quantity = amount };
            inventory.Add(newItem);
        }
    }

    public bool RemoveItem(string itemName, int amount)
    {
        InventoryItem item = inventory.Find(i => i.itemName == itemName);
        if (item != null)
        {
            if (item.quantity >= amount)
            {
                item.quantity -= amount;
                return true;
            }
        }
        return false;
    }

    public int GetQuantity(string itemName)
    {
        InventoryItem item = inventory.Find(i => i.itemName == itemName);
        return item != null ? item.quantity : 0;
    }

    public bool CheckItemsAvailability(Dictionary<string, int> requiredItems)
    {
        foreach (var kvp in requiredItems)
        {
            string itemName = kvp.Key;
            int requiredQuantity = kvp.Value;

            InventoryItem item = inventory.Find(i => i.itemName == itemName);
            if (item == null || item.quantity < requiredQuantity)
            {
                return false;
            }
        }

        return true;
    }
}