using UnityEngine;

public class Boundaries : MonoBehaviour
{
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    void Update()
    {
        if (transform.position.x < leftBoundary.position.x)
        {

            TeleportToRightSide();

        }
        else if (transform.position.x > rightBoundary.position.x)
        {
            TeleportToLeftSide();
        }
    }

    private void TeleportToLeftSide()
    {
        Vector3 newPosition = new Vector3(leftSpawnPoint.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }

    private void TeleportToRightSide()
    {
        Vector3 newPosition = new Vector3(rightSpawnPoint.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}