using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private float amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResourceManager.Instance.IncreaseResource(resourceType, amount);
            Destroy(gameObject);
        }
    }
}
