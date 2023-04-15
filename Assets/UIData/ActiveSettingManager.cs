using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ActiveSettingManager : MonoBehaviour
{
    enum E_DELAYTIMESTATE
    {
        AllAtOnce,  //������
        InTurn,     //���Ԃ�
    }

    [SerializeField, Header("�Q�[���J�n���ɔ�\���ɂ���I�u�W�F�N�g")]
    private List<GameObject> Objects;
    [SerializeField, Header("�ŏ��̃I�u�W�F�N�g���A�N�e�B�u�ɂȂ�܂ł̒x������")]
    private float DelayTime = 0.0f;
    [SerializeField, Header("���I�u�W�F�N�g���A�N�e�B�u�ɂȂ�܂ł̎���")]
    private float ActiveTime = 0.0f;
    [SerializeField, Header("�A�N�e�B�u�̎d��")]
    private E_DELAYTIMESTATE DelayState = E_DELAYTIMESTATE.AllAtOnce;
    

    //- �A�N�e�B�u����
    private bool bActive = false;
    private bool bFirstActive = false;
    //- ���ݎ���
    private float CurrentTime = 0.0f;
    private int i = 0;
    void Start()
    {
        //- �w�肵���^�O�̃I�u�W�F�N�g���擾
        foreach(GameObject o in Objects)
        {   o.SetActive(false); }
    }

    void Update()
    {
        //- �N���A�t���O�������Ă��A��A�N�e�B�u
        if (SceneChange.bIsChange && !bActive)
        {
            switch (DelayState)
            {
                //- �����ɃA�N�e�B�u������
                case E_DELAYTIMESTATE.AllAtOnce:
                    CurrentTime += Time.deltaTime;
                    //- �x�����Ԃ��o�߂�����
                    if (CurrentTime >= ActiveTime)
                    {
                        foreach (GameObject obj in Objects)
                        {
                            obj.SetActive(true);
                        }
                        bFirstActive = true;
                        bActive = true;
                    }
                    break;

                //- �x�����Ԃ��o�߂���x�ɃA�N�e�B�u������
                case E_DELAYTIMESTATE.InTurn:
                    //- �z��Ȃ���
                    CurrentTime += Time.deltaTime;

                    if(CurrentTime >= DelayTime&&!bFirstActive)
                    {
                        //- �x�����Ԃ𒴂�����A�N�e�B�u�ɂ���
                        Objects[0].SetActive(true);
                        //- �J�E���g����
                        i++;
                        //- ���ԃ��Z�b�g
                        CurrentTime = 0.0f;
                        
                        bFirstActive = true;
                    }
                    
                    //- �x�����Ԃ��o�߂����X�g�v�f���𒴂��Ă��Ȃ�
                    if (CurrentTime >= ActiveTime && i < Objects.Count && bFirstActive)
                    {
                        //- �x�����Ԃ𒴂�����A�N�e�B�u�ɂ���
                        Objects[i].SetActive(true);
                        //- �J�E���g����
                        i++;
                        //- ���ԃ��Z�b�g
                        CurrentTime = 0.0f;
                    }
                    if (i == Objects.Count)
                    {
                        //- ���ׂẴI�u�W�F�N�g���A�N�e�B�u�ɂȂ�����t���O�X�V
                        bActive = true;
                        i = 0;
                    }
                    break;

            }
            
        }

    }

    /// <summary>
    /// ���X�g�̏��߂̃I�u�W�F�N�g���A�N�e�B�u��
    /// </summary>
    /// <returns> bFirstActive </returns>
    public bool GetFirstActive()
    {
        return bFirstActive;
    }
}
