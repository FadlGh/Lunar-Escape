using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform waypoint1;
    [SerializeField] private Transform waypoint2;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float waitTime;

    private Transform targetWaypoint;
    private Rigidbody2D rb;

    private bool isPatrollingForward = true;
    private bool isFacingRight = true;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetWaypoint = waypoint1;
    }

    void FixedUpdate()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                isWaiting = false;
            }
            else
            {
                return;
            }
        }

        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, targetWaypoint.position) < stoppingDistance)
        {
            if (isPatrollingForward)
            {
                targetWaypoint = waypoint2;
            }
            else
            {
                targetWaypoint = waypoint1;
            }

            isPatrollingForward = !isPatrollingForward;
            isWaiting = true;
            waitTimer = waitTime;
            Flip();
        }
        else
        {
            rb.velocity = speed * Time.fixedDeltaTime * direction;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
