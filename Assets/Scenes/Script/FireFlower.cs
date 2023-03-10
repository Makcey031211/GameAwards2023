using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    [SerializeField, Header("火花用のオブジェクト")]
    private GameObject particleObject;

    [SerializeField, Header("爆発までの時間(秒)")]
    private float time = 3.0f;

    Rigidbody rb;

    float currentTime; // 生成からの経過時間

    public bool isExploded = false; // 爆発したかどうか
    bool isOnce; // 処理を一回だけ行う

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = 0.0f;

        isOnce = false;
    }

    // Update is called once per frame
    void Update() {
        //----- 爆発までの時間カウント
        currentTime += Time.deltaTime;
        if (currentTime >= time) {
            isExploded = true;
        }

        if (isExploded) { // 爆発した後
            if (!isOnce) { // 爆発直後
                isOnce = true;
             // 指定した位置に生成
             GameObject fire = Instantiate(
                 particleObject,                     // 生成(コピー)する対象
                 transform.position,           // 生成される位置
                 Quaternion.Euler(0.0f, 0.0f, 0.0f)  // 最初にどれだけ回転するか
                 );
                //----- 弾をそのまま当たり判定に流用する
                // 爆発時に描画をやめる
                GetComponent<MeshRenderer>().enabled = false;
                gameObject.tag = "ExplodeCollision";
                rb.isKinematic = true;
                GetComponent<DestroyTimer>().enabled = true;
                GetComponent<SphereCollider>().isTrigger = true;
            }
        }
    }

    //private void FixedUpdate() {
    //    if (isExploded) { // 爆発した後
    //        float collSize = power * 0.0004f;
    //        transform.localScale += new Vector3(collSize, collSize, collSize);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.gameObject.tag == "ExplodeCollision") {
    //        enabled = true;
    //        isExploded = true;
    //    }
    //}

    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject.tag == "ExplodeCollision") {
    //        enabled = true;
    //        isExploded = true;
    //    }
    //}
}
