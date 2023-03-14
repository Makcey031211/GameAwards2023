using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationCollision : MonoBehaviour
{
    [Header("当たり判定の広がり具合"), SerializeField]
    private float power = 3;

    [Header("削除までの時間"), SerializeField]
    private float time = 3.0f;

    float currentTime; // 生成からの経過時間

    void Start()
    {
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //----- 削除までの時間カウント
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            // 親オブジェクトごと削除
            Destroy(transform.parent.gameObject);
        }
    }

    private void FixedUpdate()
    {
        float collSize = power * 0.0004f;
        if (transform.localScale.x <= 7.5f)
        {
            transform.localScale += new Vector3(collSize, collSize, collSize);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトのタグが「花火」 又は 「打った花火」なら
        if (other.gameObject.tag == "Fireworks" ||
            other.gameObject.tag == "ShotFireworks")
        {
            // 当たったオブジェクトのSphereColliderを有効にする
            other.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
            // 当たったオブジェクトのSphereColliderを有効にする
            other.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
            // 当たったオブジェクトのFireFlowerスクリプトを有効にする
            other.gameObject.GetComponent<FireFlower>().enabled = true;
            // 当たったオブジェクトのFireFlowerスクリプト内のisExplodedをtrueに変える
            other.gameObject.GetComponent<FireFlower>().isExploded = true;
        }
    }
}
