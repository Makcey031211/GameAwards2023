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

        // �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
        UISelect.Select();
    }
}
