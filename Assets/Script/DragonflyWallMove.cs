using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyWallMove : MonoBehaviour
{
    // �ڐG�����̗񋓑�
    public enum DirectionType
    {
        Up,
        Under,
        Left,
        Rgiht,
    }

    [Header("�����蔻�����"), SerializeField]
    private DirectionType dirType;

    [Header("�ԉ΃X�N���v�g�I�u�W�F�N�g"), SerializeField]
    private GameObject moduleObj;
    private FireworksModule module; //- �ԉ΃X�N���v�g

    private bool IsWait = false; //- �ړ�����������1�t���[�������s�����߂̑ҋ@�t���O

    void Start()
    {
        //- �ԉ΃X�N���v�g�̎擾
        module = moduleObj.GetComponent<FireworksModule>();
    }
    void FixedUpdate()
    {
        IsWait = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //- �ҋ@���Ȃ烊�^�[��
        if (IsWait) return;
        //- ���������I�u�W�F�N�g�̃^�O�ɂ���ČĂяo���֐���ς���
        if (other.gameObject.tag == "Stage")               HitStage();
        if (other.gameObject.tag == "PlayerBlock") HitPlayerBlock();
    }
    void HitStage()
    {      
        //- ���g�̕��������ɂ���Ĉړ������𔽓]
        switch (dirType)
        {
            case DirectionType.Up:
                module.movedir.y *= -1;
                break;
            case DirectionType.Under:
                module.movedir.y *= -1;
                break;
            case DirectionType.Left:
                module.movedir.x *= -1;
                break;
            case DirectionType.Rgiht:
                module.movedir.x *= -1;
                break;
        }
        //- �ҋ@�t���O���I��
        IsWait = true;
    }
    void HitPlayerBlock()
    {
        Debug.Log("�ق�");
        //- ���g�̕��������ɂ���Ĉړ������𔽓]
        switch (dirType)
        {
            case DirectionType.Up:
                module.movedir.y = 0;
                if (module.movedir.x >= 0) module.movedir.x =  1;
                if (module.movedir.x <= 0) module.movedir.x = -1;
                break;
            case DirectionType.Under:
                module.movedir.y = 0;
                if (module.movedir.x >= 0) module.movedir.x = 1;
                if (module.movedir.x <= 0) module.movedir.x = -1;
                break;
            case DirectionType.Left:
                if (module.movedir.y >= 0) module.movedir.y =  1;
                if (module.movedir.y <= 0) module.movedir.y = -1;
                module.movedir.x = 0;
                break;
            case DirectionType.Rgiht:
                if (module.movedir.y >= 0) module.movedir.y = 1;
                if (module.movedir.y <= 0) module.movedir.y = -1;
                module.movedir.x = 0;
                break;
        }
        //- �ҋ@�t���O���I��
        IsWait = true;
    }
}