using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField, Header("�G�̏��񂷂�ʒu")]
    public List<Transform> wayPoints;
    [SerializeField, Header("�ړ���")]
    public float moveSpeed;
    [SerializeField, Header("���݂̈ʒu")]
    private int CurrentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoints[CurrentWaypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == wayPoints[CurrentWaypointIndex].position)
        {
            CurrentWaypointIndex++;
            if (CurrentWaypointIndex >= wayPoints.Count)
            {
                CurrentWaypointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position,
            wayPoints[CurrentWaypointIndex].position, moveSpeed * Time.deltaTime);
    }
}
