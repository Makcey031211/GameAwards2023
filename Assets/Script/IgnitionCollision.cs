using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitionCollision : MonoBehaviour
{
    [Header("�ԉ΃X�N���v�g�I�u�W�F�N�g"), SerializeField]
    private GameObject moduleObj;
    private FireworksModule module; //- �ԉ΃X�N���v�g
    [Header("��e��̖��G����(�b)"), SerializeField]
    private float InvisibleTime;


    //- �C���X�y�N�^�[�������\���ɂ���
    [SerializeField, HideInInspector]
    public bool IsDestroy = false; //- �j��t���O

    private float TimeCount = 0; //- �^�C���J�E���^

    void Start()
    {
        //- �ԉ΃X�N���v�g�̎擾
        module = moduleObj.GetComponent<FireworksModule>();
    }
    void Update()
    {
        //- �������Ă���̎��Ԃ��J�E���g
        if (module.IsExploded) TimeCount += Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        //- ���������I�u�W�F�N�g�̃^�O�ɂ���ČĂяo���֐���ς���
        if (other.gameObject.tag == "Fireworks") HitFireworks(other);
        if (other.gameObject.tag == "ExplodeCollision") HitExplodeCollision(other);

        //- �t���O�������Ă���Δj��
        if (IsDestroy) Destroy(transform.parent.gameObject);
    }
    void HitFireworks(Collider other)
    {
        //- �I�u�W�F�N�g�ɕϊ�
        GameObject obj = other.gameObject;

        //- ���������I�u�W�F�N�g��FireworksModule�̎擾
        FireworksModule module = obj.GetComponent<FireworksModule>();
        //- ���������I�u�W�F�N�g�̉ԉ΃^�C�v�ɂ���ď����𕪊�
        if (module.Type == FireworksModule.FireworksType.Boss)
            module.IgnitionBoss(obj);
        else if (module.Type != FireworksModule.FireworksType.ResurrectionPlayer)
            module.Ignition(transform.position);
        else if (module.Type == FireworksModule.FireworksType.ResurrectionPlayer)
            if (module.GetIsInv() == false)
            { module.Ignition(transform.position); }
    }
    void HitExplodeCollision(Collider other)
    {
        //- ���G���Ԓ��Ȃ烊�^�[��
        if (TimeCount <= InvisibleTime) return;
        //- ���g�̔j��t���O��ύX
        IsDestroy = true;
    }
}
