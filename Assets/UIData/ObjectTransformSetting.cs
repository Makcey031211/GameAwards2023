using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransformSetting : MonoBehaviour
{
    [SerializeField, Header("����ɔz�u�������I�u�W�F�N�g")]
    private List<GameObject> objs;

    [SerializeField, Header("�J�n�n�_�I�u�W�F�N�g")]
    private GameObject StartObj = null;

    [SerializeField, Header("���炷�Ԋu")]
    private Vector3 shift;

    [SerializeField, Header("�T�C�Y�����ꂷ��")]
    private bool size;

    void Start()
    {
        //- �J�n�n�_�̃I�u�W�F�N�g���o�^����Ă��Ȃ���
        if(StartObj == null)
        {
            //- �o�^����Ă��Ȃ�������List��0�Ԗڂ�o�^����
            StartObj = objs[0];
        }

        //- �I�u�W�F�N�g�̈ʒu�𑵂���
        objs[0].transform.position = StartObj.transform.position;

        //- �J�n�n�_�I�u�W�F�N�g���Q�l�ɁA���Ԋu�ɂ��炷
        for (int i = 0; i < objs.Count - 1; i++)
        {
            if(size)
            {
                objs[i].transform.localScale = StartObj.transform.localScale;
            }
            //- ���̃I�u�W�F�N�g�����̃I�u�W�F�N�g����ݒ萔�l�����炷
            objs[i + 1].transform.position = new Vector3(objs[i].transform.position.x + shift.x, objs[i].transform.position.y + shift.y, objs[i].transform.position.z + shift.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
