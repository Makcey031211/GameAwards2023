using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UIコンポーネントの使用

public class selectbutton : MonoBehaviour
{
    Button _next;
    Button _retry;

    void Start()
    {
        // ボタンコンポーネントの取得
        _next = GameObject.Find("/Canvas/NextSceneButton").GetComponent<Button>();
        _retry = GameObject.Find("/Canvas/RetrySceneButton").GetComponent<Button>();

        // 最初に選択状態にしたいボタンの設定
        _next.Select();
    }
}