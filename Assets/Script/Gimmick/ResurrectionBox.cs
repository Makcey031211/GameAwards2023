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

    [SerializeField, Header("�A�j���[�V��������(�b)")]
    private float animationTime = 0.1f;

    [SerializeField, Header("�A�j���[�V�����̒x������(�b)")]
    private float animationDelayTime = 0.1f;

    [SerializeField, Header("���̏��Ŏ���(�b)")]
    private float boxDisTime = 0.1f;

    [SerializeField, Header("������SE")]
    private AudioClip generatedSound;

    [SerializeField, Header("���ŉ�SE")]
    private AudioClip disSound;

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
        {//- ����������
            if (!isOnce)
            { //- ��������
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

        //- �A�j���[�V�����p�̕ϐ�
        float elapsed = 0;

        //- ���X�ɐ�������v���C���[�̐�
        int numPlayers = 1;

        //- �����ʒu(Z)
        float playerPosZ = 1.5f;

        //- �v���C���[�����X�ɐ�������
        for (int i = 0; i < numPlayers; i++)
        {
            //- �v���C���[�𐶐�����
            Vector3 spawnPosition = new Vector3(
                transform.position.x, transform.position.y, playerPosZ);
            GameObject player = Instantiate(PlayerPrefab, spawnPosition, Quaternion.identity);

            //- ���̍Đ�
            gameObject.GetComponent<AudioSource>().PlayOneShot(generatedSound);

            //- SceneChange�X�N���v�g�̃v���C���[�����t���O��true�ɂ���
            sceneChange.bIsLife = true;

            //- ���X�ɐ�������A�j���[�V����
            while (elapsed < animationTime)
            {
                float t = elapsed / animationTime;
                player.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            //- �A�j���[�V�����̒x��
            yield return new WaitForSeconds(animationDelayTime);
        }

        //- �v���C���[�𐶐���A�����������X�ɏ���
        float startTime = Time.time;
        Vector3 initialScale = transform.localScale;

        //- ���̍Đ�
        gameObject.GetComponent<AudioSource>().PlayOneShot(disSound);

        //- �����������X�ɏ��ł�����
        while (Time.time < startTime + boxDisTime)
        {
            float t = (Time.time - startTime) / boxDisTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}