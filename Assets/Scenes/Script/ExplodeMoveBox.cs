using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMoveBox : MonoBehaviour
{
    [Header("空気抵抗係数"), SerializeField]
    private float coefficient = 3;

    [Header("爆風の速さ"), SerializeField]
    private float speed = 3;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "ExplodeCollision")
        {
            var Verocity = (transform.position - other.gameObject.transform.position).normalized * speed;

            rb.AddForce(coefficient * Verocity);
        }
    }
}
