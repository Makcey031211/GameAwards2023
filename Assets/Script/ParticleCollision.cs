using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField, Header("火花SE")]
    private AudioClip sound;
    [SerializeField, Header("SEの音量")]
    private float volume;

    private bool IsOnse = false;

    private void OnParticleCollision(GameObject other)
    {
        // 当たったオブジェクトのタグが「ステージ」なら
        if (other.gameObject.tag == "Stage")
        {
            if (!IsOnse)
            {
                IsOnse = true;
                GameObject.Find("SEManager").GetComponent<SEManager>().SetPlaySE(sound,volume);
            }
        }
    }
}