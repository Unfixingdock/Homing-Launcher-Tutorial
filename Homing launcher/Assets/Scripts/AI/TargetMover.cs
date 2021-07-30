using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] Transform PointA, PointB;
    private Vector3 currentTarget;
    [SerializeField] private int Speed;

    private void Start()
    {
        transform.position = PointA.position;
        currentTarget = PointB.position;
    }

    private void FixedUpdate()
    {
        var step = Speed * Time.fixedDeltaTime;

        if (transform.position == PointA.position)
        {
            currentTarget = PointB.position;
        }
        else if (transform.position == PointB.position)
        {
            currentTarget = PointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
    }
}
