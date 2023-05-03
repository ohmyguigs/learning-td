using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 10f;
    public Animator animator;
    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        animator.SetBool("Walk Forward", true);
        
        if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint() {
        if (waypointIndex >= Waypoints.points.Length - 1) {
            Destroy(gameObject);
            // decrease life points?
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

}
