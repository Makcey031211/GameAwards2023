using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField, Header("移動量")]
    private float MoveSpeed;
    [SerializeField]
    private Transform Left;
    [SerializeField]
    private Transform Right;

    private Vector2 StartPosition;
    private int direction = 1;
    FireFlower FireflowerScript; //- 花火点火スクリプト

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
    }

    // Update is called once per frame
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
