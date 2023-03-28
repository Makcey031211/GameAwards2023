using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 当たったオブジェクトのタグが「花火」 又は 「打った花火」なら
        if (collision.gameObject.tag == "Fireworks" ||
            collision.gameObject.tag == "ShotFireworks")
        {
            // 当たったオブジェクトのFireFlowerスクリプトを有効にする
            collision.gameObject.GetComponent<FireFlower>().enabled = true;
            // 当たったオブジェクトのFireFlowerスクリプト内のisExplodedをtrueに変える
            collision.gameObject.GetComponent<FireFlower>().isExploded = true;
        }
    }
}
