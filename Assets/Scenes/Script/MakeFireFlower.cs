using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFireFlower : MonoBehaviour
{
    [SerializeField, Header("火花用のオブジェクト")]
    private GameObject particleObject;

    [SerializeField, Header("火花SE")]
    private AudioClip sound;

    Rigidbody rb;

    //- 振動用のコンポーネント
    VibrationManager vibration;

    float currentTime; // 生成からの経過時間
    bool isOnce; // 処理を一回だけ行う
    FireFlower FireflowerScript; //- 花火点火スクリプト

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = 0.0f;
        isOnce = false;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        //- 振動コンポーネントの取得
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //    //----- 爆発までの時間カウント
        //    currentTime += Time.deltaTime;
        //    if (currentTime >= time)
        //    {
        //        isExploded = true;
        //    }

        if (FireflowerScript.isExploded)
        { // 爆発した後
            if (!isOnce)
            {
                //- 振動の設定
                vibration.SetVibration(60, 1.0f);
                // 爆発直後
                isOnce = true;

                // 当たったオブジェクトのSphereColliderを有効にする
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
                // 当たったオブジェクトのSphereColliderを有効にする
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
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
                //- 音の再生
                gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            }
        }
    }
}
