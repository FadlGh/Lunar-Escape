using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float vertical;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D playerRb;

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            playerRb.gravityScale = 0f;
            playerRb.velocity = new Vector2(playerRb.velocity.x, vertical * speed);
        }
        else
        {
            playerRb.gravityScale = 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}