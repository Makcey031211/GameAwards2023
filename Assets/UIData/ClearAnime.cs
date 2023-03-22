using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ClearAnime : MonoBehaviour
{
    private enum E_Directions
    {
        TOP,
        LOWER,
        RIGHT,
        LEFT,
    }


    [SerializeField, Header("�ǂ����猻�݈ʒu�Ɍ������Ă��邩")]
    private E_Directions StartPos;
    [SerializeField, Header("�ړ������܂ł̎���:float")]
    private float MoveTime = 0.0f;
    [SerializeField, Header("�f�B���C:float")]
    private float Delay = 0.0f;
    private Vector2 InitPos;
    void Start()
    {
        RectTransform trans = GetComponent<RectTransform>();
        InitPos = trans.anchoredPosition;
        switch (StartPos)
        {
            case E_Directions.TOP:
                trans.anchoredPosition = new Vector2(InitPos.x, Screen.height);
                break;
            case E_Directions.LOWER:
                trans.anchoredPosition = new Vector2(InitPos.x, -Screen.height);
                break;
            case E_Directions.RIGHT:
                trans.anchoredPosition = new Vector2(Screen.width,InitPos.y);
                break;
            case E_Directions.LEFT:
                trans.anchoredPosition = new Vector2(-Screen.width, InitPos.y);
                break;
        }

        transform.DOLocalMove(InitPos, MoveTime)
            .SetEase(Ease.OutSine)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .SetDelay(Delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
