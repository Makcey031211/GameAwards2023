using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetAnime()
    {
        //- ScalePlayer���\�b�h��delayTime�b��ɌĂяo��
        StartCoroutine(ScalePlayer(delayTime));
    }

    private IEnumerator ScalePlayer(float deltaTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);

        float startTime = Time.time; // �J�n���Ԃ̍X�V
        Vector3 initialScale = transform.localScale; // �傫���̕ϐ�

        //- �v���C���[�����X�ɏk�߂Ă����A�j���[�V��������
        while (Time.time < startTime + shrinkTime)
        {
            float t = (Time.time - startTime) / shrinkTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }
    }
}
