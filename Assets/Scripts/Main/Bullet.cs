using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shootSpeed;
    public ParticleSystem ps;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //FindObjectOfType<AudioManager>().Play("fire");
    }

    void Update()
    {
        rb.velocity = transform.right * shootSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alien")) Destroy(collision.gameObject);

        die();
    }

    void die()
    {
        //Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}