using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator am;

    void Start()
    {
        am = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        am.SetBool("Open", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        am.SetBool("Open", false);
    }
}
