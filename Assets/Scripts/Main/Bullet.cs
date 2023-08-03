using System.Collections;
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
        //FindObjectOfType<AudioManager>().Play("explosion");
        /*if (collision.gameObject.GetComponent<HealthManager>() != null)
        {
            collision.gameObject.GetComponent<HealthManager>().ApplyDamage(25f);
        }*/


        die();
    }

    void die()
    {
        //Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}