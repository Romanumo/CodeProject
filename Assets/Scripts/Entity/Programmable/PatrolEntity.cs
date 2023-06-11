using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEntity : PracticeEntity
{
    public float speed = 1;

    [SerializeField] private Transform[] patrolPoints;
    private int patrolIndex = 0;

    private void Update()
    {
        Move();

        float distance = (transform.position - patrolPoints[patrolIndex].position).magnitude;
        if (distance < 1f)
            NextPoint();
    }

    private void NextPoint()
    {
        patrolIndex++;
        if (patrolIndex >= patrolPoints.Length)
            patrolIndex = 0;
    }

    private void Move()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, patrolPoints[patrolIndex].position, speed * Time.deltaTime);
    }
}
