using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private float amount;
    [SerializeField] private ParticleSystem ps;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DeathTimer());
        //FindObjectOfType<AudioManager>().Play("fire");
    }

    void Update()
    {
        rb.velocity = transform.right * shootSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
            collision.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        if (collision.gameObject.CompareTag("Player"))
            ResourceManager.Instance.DecreaseResource(ResourceType.Health, amount);
        die();
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
        die();
    }

    void die()
    {
        //Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}