using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UI�R���|�[�l���g�̎g�p

public class selectbutton : MonoBehaviour
{
    Button _next;
    Button _retry;

    void Start()
    {
        // �{�^���R���|�[�l���g�̎擾
        _next = GameObject.Find("/Canvas/NextSceneButton").GetComponent<Button>();
        _retry = GameObject.Find("/Canvas/RetrySceneButton").GetComponent<Button>();

        // �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
        _next.Select();
    }
}