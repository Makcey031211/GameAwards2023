using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UI�R���|�[�l���g�̎g�p

public class selectbutton : MonoBehaviour
{
    Button UISelect;
    [SerializeField, Header("UI�̑I��")]
    private GameObject UIAnimeComplete;

    void Start()
    {
        // �{�^���R���|�[�l���g�̎擾
        UISelect = UIAnimeComplete.GetComponent<Button>();

        // �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
        UISelect.Select();
    }
}