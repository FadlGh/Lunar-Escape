using UnityEngine;

public class BaseEntryController : MonoBehaviour
{
    public bool playerInsideBase = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInsideBase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInsideBase = false;
        }
    }
}
