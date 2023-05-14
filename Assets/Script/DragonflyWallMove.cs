using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyWallMove : MonoBehaviour
{
    // 接触方向の列挙体
    public enum DirectionType
    {
        Up,
        Under,
        Left,
        Rgiht,
    }

    [Header("当たり判定方向"), SerializeField]
    private DirectionType dirType;

    [Header("花火スクリプトオブジェクト"), SerializeField]
    private GameObject moduleObj;
    private FireworksModule module; //- 花火スクリプト

    private bool IsWait = false; //- 移動方向処理を1フレームだけ行うための待機フラグ

    void Start()
    {
        //- 花火スクリプトの取得
        module = moduleObj.GetComponent<FireworksModule>();
    }
    void FixedUpdate()
    {
        IsWait = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //- 当たったオブジェクトのタグが"Stage"でなければリターン
        if (other.gameObject.tag != "Stage") return;
        //- 待機中ならリターン
        if (IsWait) return;

        //- 自身の方向属性によって移動方向を反転
        switch(dirType)
        {
            case DirectionType.Up:
                module.movedir.y *= -1;
                IsWait = true;
                break;
            case DirectionType.Under:
                module.movedir.y *= -1;
                IsWait = true;
                break;
            case DirectionType.Left:
                module.movedir.x *= -1;
                IsWait = true;
                break;
            case DirectionType.Rgiht:
                module.movedir.x *= -1;
                IsWait = true;
                break;
        }
    }
}