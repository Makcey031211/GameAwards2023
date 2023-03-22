using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSettingManager : MonoBehaviour
{
    [SerializeField, Header("�Q�[���J�n���ɔ�\���ɂ���I�u�W�F�N�g")]
    private List<GameObject> Objects;

    private bool bActive = false;
    void Start()
    {
        
        //- �w�肵���^�O�̃I�u�W�F�N�g���擾
        foreach(GameObject o in Objects)
        {
            o.SetActive(false);
        }

    }

    void Update()
    {
        if (SceneChange.bIsChange && !bActive)
        {
            foreach(GameObject o in Objects)
            {
                o.SetActive(true);
            }
            bActive = true;
        }
    }

}
