using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������A����������v���C���[���Ăѐ�������
/// </summary>
public class ResurrectionBox : MonoBehaviour
{
    [SerializeField, Header("��������I�u�W�F�N�g")]
    private GameObject PlayerPrefab;

    [SerializeField, Header("�����܂ł̑҂�����(�b)")]
    private float delayTimer = 0.1f;

    FireFlower FireflowerScript; //- �ԉΓ_�΃X�N���v�g

    float delayTime; //- �����܂ł̑҂�����(�b)

    bool isOnce; //- ��������񂾂��s��

    private void Start()
    {
        delayTime = delayTimer;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
    }

    private void Update()
    {
        if (FireflowerScript.isExploded)
        {//����������
            if (!isOnce)
            { // ��������
                isOnce = true;
                //- SpawnPlayer���\�b�h��delayTime�b��ɌĂяo��
                StartCoroutine(SpawnPlayer(delayTime));
            }
        }
    }

    private IEnumerator SpawnPlayer(float delayTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);
        //- �v���C���[�𐶐�����
        Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        //- �v���C���[�𐶐���A���������폜
        Destroy(gameObject);
    }
}