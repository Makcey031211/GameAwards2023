using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSettingManager : MonoBehaviour
{
    [SerializeField, Header("ゲーム開始時に非表示にするオブジェクト")]
    private List<GameObject> Objects;

    private bool bActive = false;
    void Start()
    {
        
        //- 指定したタグのオブジェクトを取得
        foreach(GameObject o in Objects)
        {
            o.SetActive(false);
        }

    }

    void Update()
    {
        if (SceneChange.bIsChange && !bActive)
        {
            foreach(GameObject o in Objects)
            {
                o.SetActive(true);
            }
            bActive = true;
        }
    }

}
