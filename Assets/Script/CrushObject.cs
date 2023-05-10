using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// アタッチしたオブジェクトが判定をとるオブジェクトに指定したオブジェクト同士に挟まれたら
// 爆発させるオブジェクトを爆発させる

public class CrushObject : MonoBehaviour
{
    [Header("判定をとるオブジェクト"), SerializeField]
    private GameObject obj1;
    [SerializeField]
    private GameObject obj2;

    [Header("爆発させるオブジェクト"), SerializeField]
    private FireworksModule fireworks;
    [Header("その他"), SerializeField]
    private PController pcontroller;
    [SerializeField]
    private SceneChange sceneChange;
    

    // 衝突しているオブジェクトリスト
    private List<GameObject> hitObjects = new List<GameObject>();

    private bool checkobj1;
    private bool checkobj2;

    void Start()
    {
        checkobj1 = false;
        checkobj2 = false;
    }

    void Update()
    {
        if (hitObjects.Count >= 2)
        {
            // TODO:2つ以上同時に衝突していた場合の処理
            //- 爆発処理
            fireworks.Ignition(transform.position);

            //- SceneChangeスクリプトのプレイヤー生存フラグをfalseにする
            sceneChange.bIsLife = false;
        }

        // 毎回衝突したオブジェクトリストをクリアする
        hitObjects.Clear();
        checkobj1 = false;
        checkobj2 = false;
    }

    void OnTriggerStay(Collider other)
    {
        // 衝突しているオブジェクトをリストに登録する
        if (other.name == obj1.name && checkobj1 == false)
        {
            hitObjects.Add(other.gameObject);
            Debug.Log(other.name);
            checkobj1 = true;
        }

        if (other.name == obj2.name && checkobj2 == false)
        {
            hitObjects.Add(other.gameObject);
            Debug.Log(other.name);
            checkobj2 = true;
        }

    }
    void OnCollisionStay(Collision other)
    {
        // 衝突しているオブジェクトをリストに登録する
        if (other.gameObject.name == obj1.name && checkobj1 == false)
        {
            hitObjects.Add(other.gameObject);
            Debug.Log(other.gameObject.name);
            checkobj1 = true;
        }

        if (other.gameObject.name == obj2.name && checkobj2 == false)
        {
            hitObjects.Add(other.gameObject);
            Debug.Log(other.gameObject.name);
            checkobj2 = true;
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Stage")
    //    {
    //        Debug.Log("Hit");
    //        //OnTriggerEnter()
    //    }
    //}
}
