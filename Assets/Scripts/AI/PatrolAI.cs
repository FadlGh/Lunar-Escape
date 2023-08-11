using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform waypoint1;
    [SerializeField] private Transform waypoint2;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float waitTime;
    [SerializeField] private LayerMask playerLayer;

    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;
    private float timeBetweenCounter;
    private float timeBetween = 1f;

    [Header("Die")]
    [SerializeField] private List<GameObject> items;

    private Transform targetWaypoint;
    private Rigidbody2D rb;
    private Animator am;

    private bool isPatrollingForward = true;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private bool canShoot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
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
                Flip();
            }
            else
            {
                return;
            }
        }

        bool canSeePlayer = isPatrollingForward ? Physics2D.Raycast(transform.position, Vector2.right, 5f, playerLayer) : Physics2D.Raycast(transform.position, Vector2.left, 5f, playerLayer);

        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        if (!canSeePlayer)
        {
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
            }
            else
            {
                rb.velocity = speed * Time.fixedDeltaTime * direction;
            }
        }

        am.SetFloat("Speed", rb.velocity.sqrMagnitude - 0.3f);

        if (timeBetweenCounter > 0)
        {
            timeBetweenCounter -= Time.deltaTime;
            return;
        }

        if (canSeePlayer)
        {
            float bulletRotationZ = isPatrollingForward ? 0f : 180f;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, bulletRotationZ);
            if (!canShoot) return;
            Instantiate(bullet, shootPoint.position, bulletRotation);
            timeBetweenCounter = timeBetween;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void DisableComponents()
    {
        canShoot = false;
        GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>().Play("Scream");
    }

    public void Die()
    { 
        Instantiate(items[Random.Range(0, items.Count)], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
