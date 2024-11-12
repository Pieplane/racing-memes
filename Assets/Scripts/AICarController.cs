using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    public List<Transform> waypoints;
    private int currentWaypointIndex = 0;

    public float acceleration = 20f;
    public float maxSpeed = 50f;
    public float turnSensitivity = 2f;
    public Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        NavigateToNextWaypoint();
        SteerTowardsWaypoint();
        ControlSpeed();
    }

    void NavigateToNextWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);

        if (distanceToWaypoint < 5.0f) // Если близко к контрольной точке, перейти к следующей
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }

    void SteerTowardsWaypoint()
    {
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        float steer = Vector3.Dot(transform.right, direction) * turnSensitivity;
        carRb.MoveRotation(Quaternion.Euler(new Vector3(0, steer, 0) * Time.deltaTime));
    }

    void ControlSpeed()
    {
        if (carRb.velocity.magnitude < maxSpeed)
        {
            carRb.AddForce(transform.forward * acceleration * Time.deltaTime, ForceMode.Acceleration);
        }
    }
}
