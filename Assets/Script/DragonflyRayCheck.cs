using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragonflyRayCheck : MonoBehaviour
{
    [Header("�ԉ΃X�N���v�g�I�u�W�F�N�g"), SerializeField]
    private GameObject moduleObj;
    private FireworksModule module; //- �ԉ΃I�u�W�F�N�g�ɃA�^�b�`����Ă���X�N���v�g

    [Header("���CBOX�̔�������(���CBOX�̈�ӂ̒���)"), SerializeField]
    private float RayBoxRadius;

    [Header("������������ɂȂ郌�C�̒���"), SerializeField]
    private float HitRayDistance;

    private Vector2[] RayStartPos;  //- ���C�̔������W���i�[����z��
    private Vector2[] RayDirection; //- ���C�̔����������i�[����z��

    void Start()
    {
        //- �ԉ΃X�N���v�g�̎擾
        module = moduleObj.GetComponent<FireworksModule>();

        //- ���C�̔������W�z��𐶐�
        RayStartPos = new Vector2[5];
        RayStartPos[0] = new Vector2(-1, 1);
        RayStartPos[1] = new Vector2( 1, 1);
        RayStartPos[2] = new Vector2( 1,-1);
        RayStartPos[3] = new Vector2(-1,-1);
        RayStartPos[4] = RayStartPos[0]; //- �Ō�ƍŏ��̍��W�͓����Ȃ̂ŁA�������W��p�ӂ��Ă���

        //- ���C�̔��������z��𐶐�
        RayDirection = new Vector2[4];
        RayDirection[0] = new Vector2( 0,  1);
        RayDirection[1] = new Vector2( 1,  0);
        RayDirection[2] = new Vector2( 0, -1);
        RayDirection[3] = new Vector2(-1,  0);
    }
    
    void Update()
    {
        //- ���g�̍��W���擾
        Vector3 MyPos = this.transform.position;

        // === �S�̊p����v�W�{�̃��C���΂����� ===
        for (int i = 0; i < 8; i++)
        {
            // === ���C�p�ϐ��p�ӕ��� === 
            //- ���C�̔������W�����CBOX�T�C�Y���������炷
            Vector2 PosRadius = new Vector2(RayBoxRadius / 2 * RayStartPos[(i + 1) / 2].x, RayBoxRadius / 2 * RayStartPos[(i + 1) / 2].y);
            //- ���C�̔������W�����߂�
            Vector3 NowPos = new Vector3(MyPos.x + PosRadius.x, MyPos.y + PosRadius.y, 0);
            //- ���C�̕���
            int DirNum = i / 2; //- ���C�����p�̔z��ԍ��ϐ�
            Vector3 NowDir = RayDirection[DirNum];

            //- ���C�𐶐�
            Ray ray = new Ray(NowPos, NowDir);

            //- �����蔻��̃f�o�b�O�\��
            if (Input.GetKey(KeyCode.Alpha1) || true)
            {
                Debug.DrawRay(NowPos, NowDir * 5, Color.red);     //�ԐF�łT�b�ԉ���
                Debug.DrawRay(NowPos, NowDir * HitRayDistance, Color.blue); //�F�łT�b�ԉ���
            }

            //// ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
            //if (Physics.Raycast(ray, out hit))
            //{
            
            //- �ђʂ��郌�C���΂��A���������I�u�W�F�N�g��S�Ē��ׂ�
            foreach (RaycastHit hit in Physics.RaycastAll(ray, HitRayDistance))
            {
                //- ���C�����������������q�b�g���苗����������΁A�R���e�j���[
                if (hit.distance >= HitRayDistance)          continue;
                //- ���������I�u�W�F�N�g�̃^�O��"Stage"�łȂ���΁A�R���e�j���[
                if (hit.collider.gameObject.tag != "Stage")  continue;
                
                // === ���C�̕����𒲂ׂ� ===
                //- ����� & �g���{�ԉ΂���Ɉړ���
                if (DirNum == 0 && module.movedir.y > 0)
                { module.movedir.y *= -1;
                }

                //- �E���� & �g���{�ԉ΂��E�Ɉړ���
                if (DirNum == 1 && module.movedir.x > 0)
                { module.movedir.x *= -1;
                }

                //- ������ & �g���{�ԉ΂����Ɉړ���
                if (DirNum == 2 && module.movedir.y < 0)
                { module.movedir.y *= -1;
                }

                //- ������ & �g���{�ԉ΂����Ɉړ���
                if (DirNum == 3 && module.movedir.x < 0)
                { module.movedir.x *= -1;
                }
            }
        }

        // === ��ʊO�j�󏈗� ===

    }
}
