using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField, Header("火花SE")]
    private AudioClip sound;

    private void OnParticleCollision(GameObject other)
    {
        // 当たったオブジェクトのタグが「ステージ」なら
        if (other.gameObject.tag == "Stage")
        {
            transform.parent.gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }
}