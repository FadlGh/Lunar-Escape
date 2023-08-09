using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBar : MonoBehaviour
{
    [SerializeField] private Animator bar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bar.SetBool("Open", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bar.SetBool("Open", false);
    }
}
