using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定した区間を自動移動
/// </summary>
public class AutoMove : MonoBehaviour
{
    [SerializeField, Header("移動量")]
    private float MoveSpeed;
    [SerializeField]
    private Transform Left;
    [SerializeField]
    private Transform Right;

    //- 開始位置
    private Vector2 StartPosition;

    //- 方向
    private int direction = 1;

    //- 花火点火スクリプト
    FireFlower FireflowerScript; 

    void Start()
    {
        StartPosition = transform.position;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
    }

    void Update()
    {
        if (!FireflowerScript.isExploded)
        {
            //- 右方向へ一定地点たどり着いたら左方向へ
            if (transform.position.x >= Right.position.x)
            {
                direction = -1;
            }
            //- 左方向へ一定地点たどり着いたら右方向へ
            if (transform.position.x <= Left.position.x)
            {
                direction = 1;
            }
            //- 跳ね返り処理
            transform.position = new Vector2(Mathf.Sin(Time.time) * MoveSpeed +
                StartPosition.x, StartPosition.y);
        }
    }
}
