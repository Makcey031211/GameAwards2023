using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLine : MonoBehaviour
{
    [SerializeField, Header("ステージ遷移用ボタンリスト")]
    private List<GameObject> StageList;
    [SerializeField, Header("ステージ間ライン")]
    private LineRenderer line;
    [SerializeField, Header("線の太さ")]
    private float width = 0.2f;

    private void LineRender()
    {
        line.startWidth = width;
        line.endWidth = width;

        line.startColor = Color.yellow;
        line.endColor = Color.yellow;

        line.positionCount = StageList.Count;
        int LineCount = 0;
        foreach(GameObject sl in StageList)
        {
            line.SetPosition(LineCount, sl.transform.position);
            LineCount++;
        }
    }

    private void Awake()
    {
        LineRender();
    }
}
