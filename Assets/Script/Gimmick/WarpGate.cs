using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SerializeField, Header("ワープする位置")]
    private Transform warpPoint;
    [SerializeField, Header("ワープゲートの範囲")]
    private float radius = 1.0f;
    [SerializeField, Header("吸い込まれる時の速度(秒)")]
    private float suctionSpeed = 1.0f;
    [SerializeField, Header("吐き出される時の速度(秒)")]
    private float spittingSpeed = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        //- ワープホールに接触したら指定した位置にワープする
        if (other.gameObject.tag == "Untagged")
        {
            //- プレイヤーとワープゲートの距離を計算
            Vector3 playerPos = other.transform.position;
            Vector3 gatePos   = transform.position;
            float distance = Vector3.Distance(playerPos, gatePos);

            //- プレイヤーをワープする処理
            if (distance < radius)
            {
                //- プレイヤーを指定した位置にワープさせる
                other.transform.position = warpPoint.position;

                //- ワープ後に少し時間をおいてから、吐き出させる演出をする
                Invoke("Untagged", spittingSpeed);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //- ワープホールに触れたら指定した位置にワープする
        if (other.gameObject.tag == "Untagged")
        {
            //- プレイヤーとワープゲートの距離を計算
            Vector3 playerPos = other.transform.position;
            Vector3 gatePos   = transform.position;
            float distance    = Vector3.Distance(playerPos, gatePos);

            //- プレイヤーをワープする処理
            if (distance < radius)
            {
                //- プレイヤーをワープゲートに向かって吸い寄せる
                Vector3 surtionDir = (gatePos - playerPos).normalized;
                other.transform.position += surtionDir * suctionSpeed * Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// ワープ先の吐き出される場所を求める関数
    /// </summary>
    private void SpitOutPlayer()
    {
        //- 吐き出される方向を決定
        Vector3 spitDir = (warpPoint.position - transform.position).normalized;

        //- プレイヤーに速度を加えて演出する
        Rigidbody playerRb = GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.velocity = spitDir * spittingSpeed;
        }
    }
}
