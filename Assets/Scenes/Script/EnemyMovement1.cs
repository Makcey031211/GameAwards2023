using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField, Header("敵の巡回する位置")]
    public List<Transform> wayPoints;
    [SerializeField, Header("移動量")]
    public float moveSpeed;
    [SerializeField, Header("現在の位置")]
    private int CurrentWaypointIndex = 0;

    FireFlower FireflowerScript; //- 花火点火スクリプト

    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoints[CurrentWaypointIndex].position;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FireflowerScript.isExploded)
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
}
