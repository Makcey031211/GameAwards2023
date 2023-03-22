using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UIコンポーネントの使用

public class selectbutton : MonoBehaviour
{
    Button UISelect;
    [SerializeField, Header("UIの選択")]
    private GameObject UIAnimeComplete;

    void Start()
    {
        // ボタンコンポーネントの取得
        UISelect = UIAnimeComplete.GetComponent<Button>();

        // 最初に選択状態にしたいボタンの設定
        UISelect.Select();
    }
}