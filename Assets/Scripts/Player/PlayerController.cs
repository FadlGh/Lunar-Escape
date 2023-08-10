using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem dust;

    [Header("Jump Settings")]
    [SerializeField] private float jumpingPower;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;
    private float timeBetweenCounter;
    private float timeBetween = 1f;

    private float jumpBufferTimeCounter;

    private float coyoteTimeCounter;

    private bool isFacingRight = true;
    private float horizontal;

    private Animator am;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimeCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        if (jumpBufferTimeCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferTimeCounter = 0f;
            CreateDust();
        }

        if (rb.velocity.y < 0f)
        {
            rb.velocity += 1.5f * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (rb.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            rb.velocity += 1f * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            float bulletRotationZ = isFacingRight ? 0f : 180f;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, bulletRotationZ);
            Instantiate(bullet, shootPoint.position, bulletRotation);
            timeBetweenCounter = timeBetween;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        am.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            if (IsGrounded()) CreateDust();
        }
    }

    private void CreateDust()
    {
        dust.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, 0.02f);
    }
}