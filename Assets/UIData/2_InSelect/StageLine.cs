using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLine : MonoBehaviour
{
    [SerializeField, Header("ステージ遷移用ボタンリスト")]
    private List<GameObject> StageList;
    [SerializeField, Header("ステージ間ライン")]
    private LineRenderer line;

    private void LineRender()
    {
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
