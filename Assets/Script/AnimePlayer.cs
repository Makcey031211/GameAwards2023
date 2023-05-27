using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 *  ����F����
 *  �T�v�F�����̃v���C���[����
 */
public class AnimePlayer : MonoBehaviour
{
    [SerializeField, Header("�X�P�[���ύX�I�u�W�F�N�g")]
    private GameObject scaleObj;
    [SerializeField, Header("�x������(�b)")]
    private float delayTime;
    [SerializeField, Header("�k��ł�������(�b)")]
    private float shrinkTime;

    //- �{�^����������Ă��邩�ǂ���
    bool bIsPushAnime = false;

    public void SetAnime()
    {
        //- ScalePlayer���\�b�h��delayTime�b��ɌĂяo��
        StartCoroutine(ScalePlayer(delayTime));
    }

    private IEnumerator ScalePlayer(float delayTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);

        //- �A�j���[�V�����̊J�n������ݒ�
        float startTime      = Time.time;
        //- �������̏����X�P�[����ݒ�
        Vector3 initialScale = transform.localScale;

        //=== �v���C���[�����X�ɏk�߂Ă����A�j���[�V���� ===
        //- �w�肵���b�����A�A�j���[�V����������
        while (Time.time < startTime + shrinkTime)
        {
            //- �A�j���[�V�����̐i�s�x���v�Z
            float t = (Time.time - startTime) / shrinkTime;
            //- �X�P�[�������X�ɕω�������
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            //- ���̃t���[���܂őҋ@
            yield return null;
        }

        //- �k���A�j���[�V�������I��������I�u�W�F�N�g���\���ɂ���
        scaleObj.SetActive(false);
    }

    /// <summary>
    /// �R���g���[���[�擾�֐�
    /// </summary>
    /// <param name="context"></param>
    public void OnAnime(InputAction.CallbackContext context)
    {
        //- �{�^����������Ă���ԁA�ϐ���ݒ�
        if (context.started)  { bIsPushAnime = true; }
        if (context.canceled) { bIsPushAnime = false; }
    }
}