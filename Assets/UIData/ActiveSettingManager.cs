using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSettingManager : MonoBehaviour
{

    [SerializeField, Header("�Q�[���J�n���ɔ�\���ɂ���I�u�W�F�N�gA")]
    private GameObject Object_A;
    [SerializeField, Header("�Q�[���J�n���ɔ�\���ɂ���I�u�W�F�N�gB")]
    private GameObject Object_B;

    private bool bActive = false;
    void Start()
    {
        
        //- �w�肵���^�O�̃I�u�W�F�N�g���擾
        Object_A.SetActive(false);
        Object_B.SetActive(false);
    }

    void Update()
    {
        if (SceneChange.bIsChange && !bActive)
        {
            Object_A.SetActive(true);
            Object_B.SetActive(true);
            bActive = true;
        }
    }

}
