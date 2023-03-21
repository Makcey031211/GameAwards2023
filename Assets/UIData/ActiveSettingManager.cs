using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSettingManager : MonoBehaviour
{

    [SerializeField, Header("ゲーム開始時に非表示にするオブジェクトA")]
    private GameObject Object_A;
    [SerializeField, Header("ゲーム開始時に非表示にするオブジェクトB")]
    private GameObject Object_B;

    private bool bActive = false;
    void Start()
    {
        
        //- 指定したタグのオブジェクトを取得
        Object_A.SetActive(false);
        Object_B.SetActive(false);
    }

    void Update()
    {
        if (SceneChange.bIsChange && !bActive)
        {
            Object_A.SetActive(true);
            Object_B.SetActive(true);
            bActive = true;
        }
    }

}
