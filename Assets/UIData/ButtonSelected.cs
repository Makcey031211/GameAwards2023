using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI�R���|�[�l���g�̎g�p

public class ButtonSelected : MonoBehaviour
{
    Button UISelect;
    void Start()
    {
        UISelect = GetComponent<Button>();
        // �{�^���I�������Ȃ�Ȃ��悤�ɂ���
        GetComponent<ButtonAnime>().bPermissionSelectSE = false;
        // �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
        UISelect.Select();
    }
}
