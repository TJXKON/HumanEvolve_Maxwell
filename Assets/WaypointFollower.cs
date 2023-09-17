using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float moveSpeed = 2.0f;
    private bool movingToWaypoint2 = true;

    private void Update()
    {
        // Determine the target waypoint based on current direction.
        Transform targetWaypoint = movingToWaypoint2 ? waypoint2 : waypoint1;

        // Move towards the target waypoint.
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Check if we've reached the target waypoint.
        if (transform.position == targetWaypoint.position)
        {
            // Toggle direction.
            movingToWaypoint2 = !movingToWaypoint2;
        }
    }
}
