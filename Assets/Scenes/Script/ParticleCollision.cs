using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        // 当たったオブジェクトのタグが「花火」 又は 「打った花火」なら
        if (other.gameObject.tag == "Fireworks" ||
            other.gameObject.tag == "ShotFireworks")
        {
            // 当たったオブジェクトのFireFlowerスクリプトを有効にする
            other.gameObject.GetComponent<FireFlower>().enabled = true;
            // 当たったオブジェクトのFireFlowerスクリプト内のisExplodedをtrueに変える
            other.gameObject.GetComponent<FireFlower>().isExploded = true;
        }
    }
}