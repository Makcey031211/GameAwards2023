using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIコンポーネントの使用

public class ButtonSelected : MonoBehaviour
{
    Button UISelect;
    void Start()
    {
        UISelect = GetComponent<Button>();
        // ボタン選択音がならないようにする
        GetComponent<ButtonAnime>().bPermissionSelectSE = false;
        // 最初に選択状態にしたいボタンの設定
        UISelect.Select();
    }
}
