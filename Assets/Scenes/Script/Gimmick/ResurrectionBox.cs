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

    [SerializeField, Header("������SE")]
    private AudioClip sound;

    //- �ԉΓ_�΃X�N���v�g
    FireFlower FireflowerScript;

    //- �����܂ł̑҂�����(�b)
    float delayTime;

    //- ��������񂾂��s��
    bool isOnce;

    private GameObject CameraObject;
    SceneChange sceneChange;

    private void Start()
    {
        delayTime = delayTimer;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        CameraObject = GameObject.Find("Main Camera");
        sceneChange = CameraObject.GetComponent<SceneChange>();
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
        //- ���̍Đ�
        gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
        //- SceneChange�X�N���v�g�̃v���C���[�����t���O��true�ɂ���
        sceneChange.bIsLife = true;
        //- �v���C���[�𐶐���A���������폜
        Destroy(gameObject);
    }
}