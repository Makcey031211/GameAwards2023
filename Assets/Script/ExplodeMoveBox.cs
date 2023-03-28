using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMoveBox : MonoBehaviour
{
    [Header("空気抵抗係数"), SerializeField]
    private float coefficient = 3;

    [Header("爆風の速さ"), SerializeField]
    private float speed = 3;

    [SerializeField, Header("風SE")]
    private AudioClip sound;

    [SerializeField, Header("減速フラグ (trueで減速)")]
    private bool SlowDown = true;

    [SerializeField, Header("減速度 (値が大きいほど減速する)")]
    private float Deceleration = 0.005f;

    Rigidbody rb;

    //- 減速するかのフラグ
    private bool bIsDrag;

    //- 一度だけ処理をする用のフラグ
    private bool IsOnce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bIsDrag = false;
        IsOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SlowDown && bIsDrag)
        {
            //- 徐々に減速
            rb.drag += Deceleration;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "ExplodeCollision")
        {
            var Verocity = (transform.position - other.gameObject.transform.position).normalized * speed;

            rb.AddForce(coefficient * Verocity);

            //- 一度だけ処理をする
            if (!IsOnce)
            {
                IsOnce = true;
                //- 音の再生
                gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            }

            //- 減速フラグをtrueに変える
            bIsDrag = true;
        }
    }
}
