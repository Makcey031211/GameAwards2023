using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntryAnime : MonoBehaviour
{
    private enum E_OUTDIRECTION
    {
        [Header("左")]
        LEFT,
        [Header("右")]
        RIGHT,
    }

    private const float LEFT = -300.0f;
    private const float RIGHT = 2500.0f;

    [SerializeField,Header("登場時の移動開始までの時間")]
    private float DelayTime;
    [SerializeField, Header("登場時の移動時間")]
    private float MoveTime;
    [SerializeField, Header("退場時の方向")]
    private E_OUTDIRECTION direction = E_OUTDIRECTION.LEFT;
    [SerializeField, Header("退場時の移動時間")]
    private float EndMoveTime;

    private Vector3 pos;
    private bool first = false;

    
    private void Awake()
    {
        //- 配置位置を記憶
        pos = transform.position;
        //- 初期位置を真下に移動させる
        transform.position = new Vector3(pos.x, -150.0f, pos.z);
    }

    void Start()
    {        
        //- 画面内に登場する
        transform.DOMoveY(pos.y, MoveTime).SetDelay(DelayTime);
    }

    
    void Update()
    {
        if(SceneChange.bIsChange && !first)
        {
            switch (direction)
            {
                case E_OUTDIRECTION.LEFT:
                    transform.DOMoveX(LEFT, EndMoveTime).OnComplete(()=> {
                        Destroy(gameObject);
                    });
                    first = true;
                    break;
                case E_OUTDIRECTION.RIGHT:
                    transform.DOMoveX(RIGHT,EndMoveTime).OnComplete(() => {
                        Destroy(gameObject);
                    });
                    first = true;
                    break;
            }
         
        }
    }
}
