using UnityEngine.UI;
using UnityEngine;

public class ReadClearFlag : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W��")]
    private int stagenum = -1;
    [SerializeField, Header("����������")]
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
            //Debug.Log("�ǂݍ���" + "," + stagenum + "," + save.GetStageClear(stagenum));
            //- �N���A���Ă��Ȃ���
            if (stagenum > 0 && !save.GetStageClear(stagenum))
            {
                //- �N���A���Ă��Ȃ���Δ�A�N�e�B�u�ɂ���
                obj.SetActive(false);
            }
            else if (stagenum > 0 && save.GetStageClear(stagenum))
            {
                //- �N���A���Ă���Γǂݍ��ݔ����ύX����
                read = true;
            }
            first = true;
        }

        //- �N���A�ǂݍ��݂����Ă��Ȃ��A�N���A�t���O�������Ă���
        if (stagenum > 0 && !read && save.GetStageClear(stagenum))
        {
            //- �{�^�����A�N�e�B�u�ɂ���
            obj.SetActive(true);
            //- �ǂݍ��݃t���O�ύX
            read = true;
        }
    }
}
