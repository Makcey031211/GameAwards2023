using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ActiveSettingManager : MonoBehaviour
{
    enum E_DELAYTIMESTATE
    {
        AllAtOnce,  //同時に
        InTurn,     //順番に
    }

    [SerializeField, Header("ゲーム開始時に非表示にするオブジェクト")]
    private List<GameObject> Objects;
    [SerializeField, Header("アクティブになるまでの遅延時間")]
    private float DelayTime = 0.0f;
    [SerializeField, Header("アクティブの仕方")]
    private E_DELAYTIMESTATE DelayState = E_DELAYTIMESTATE.AllAtOnce;
    

    //- アクティブ判定
    private bool bActive = false;
    private bool bFirstActive = false;
    //- 現在時間
    private float CurrentTime = 0.0f;
    private int i = 0;
    void Start()
    {
        //- 指定したタグのオブジェクトを取得
        foreach(GameObject o in Objects)
        {   o.SetActive(false); }
    }

    void Update()
    {
        //- クリアフラグが立ってかつ、非アクティブ
        if (SceneChange.bIsChange && !bActive)
        {
            switch (DelayState)
            {
                //- 同時にアクティブ化する
                case E_DELAYTIMESTATE.AllAtOnce:
                    CurrentTime += Time.deltaTime;
                    //- 遅延時間が経過したか
                    if (CurrentTime >= DelayTime)
                    {
                        foreach (GameObject obj in Objects)
                        {
                            obj.SetActive(true);
                        }
                        bFirstActive = true;
                        bActive = true;
                    }
                    break;

                //- 遅延時間が経過する度にアクティブ化する
                case E_DELAYTIMESTATE.InTurn:
                    CurrentTime += Time.deltaTime;
                    //- 遅延時間が経過かつリスト要素数を超えていない
                    if (CurrentTime >= DelayTime && i < Objects.Count)
                    {
                        //- 遅延時間を超えたらアクティブにする
                        Objects[i].SetActive(true);
                        //- カウント増加
                        i++;
                        //- 時間リセット
                        CurrentTime = 0.0f;
                        if(i == 0)
                        {
                            //- はじめのオブジェクトがアクティブになる
                            bFirstActive = true;
                        }
                    }
                    if (i == Objects.Count)
                    {
                        //- すべてのオブジェクトがアクティブになったらフラグ更新
                        bActive = true;
                    }
                    break;
            }
            
        }

    }

    public bool GetFirstActive()
    {
        return bFirstActive;
    }
}
