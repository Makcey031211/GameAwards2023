using UnityEngine.UI;
using UnityEngine;

public class ReadClearFlag : MonoBehaviour
{
    [SerializeField, Header("ステージ数")]
    private int stagenum = -1;
    [SerializeField, Header("自分を入れる")]
    private GameObject obj;
    private SaveManager save;
    private bool read = false;
    private bool first = false;
    void Update()
    {

        if(!first)
        {
            save = FindObjectOfType<SaveManager>();
            //print(name);
            //Debug.Log("読み込み" + "," + stagenum + "," + save.GetStageClear(stagenum));
            //- クリアしていないか
            if (stagenum > 0 && !save.GetStageClear(stagenum))
            {
                //- クリアしていなければ非アクティブにする
                obj.SetActive(false);
            }
            else if (stagenum > 0 && save.GetStageClear(stagenum))
            {
                //- クリアしていれば読み込み判定を変更する
                read = true;
            }
            first = true;
        }

        //- クリア読み込みをしていない、クリアフラグが立っている
        if (stagenum > 0 && !read && save.GetStageClear(stagenum))
        {
            //- ボタンをアクティブにする
            obj.SetActive(true);
            //- 読み込みフラグ変更
            read = true;
        }
    }
}
