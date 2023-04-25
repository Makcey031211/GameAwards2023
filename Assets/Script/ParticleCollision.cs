using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private bool IsOnce = false;

    private void OnParticleCollision(GameObject other)
    {
        // 当たったオブジェクトのタグが「ステージ」なら
        if (other.gameObject.tag == "Stage")
        {
            if (!IsOnce)
            {
                IsOnce = true;
                //- 火花音の再生
                SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Spark);
            }
        }
    }
}