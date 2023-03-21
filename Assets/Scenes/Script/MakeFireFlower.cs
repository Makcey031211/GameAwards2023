using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeFireFlower : MonoBehaviour
{
    [SerializeField, Header("�Ήԗp�̃I�u�W�F�N�g")]
    private GameObject particleObject;

    [SerializeField, Header("�Ή�SE")]
    private AudioClip sound;

    Rigidbody rb;

    //- �U���p�̃R���|�[�l���g
    VibrationManager vibration;

    float currentTime; // ��������̌o�ߎ���
    bool isOnce; // ��������񂾂��s��
    FireFlower FireflowerScript; //- �ԉΓ_�΃X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = 0.0f;
        isOnce = false;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        //- �U���R���|�[�l���g�̎擾
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //    //----- �����܂ł̎��ԃJ�E���g
        //    currentTime += Time.deltaTime;
        //    if (currentTime >= time)
        //    {
        //        isExploded = true;
        //    }

        if (FireflowerScript.isExploded)
        { // ����������
            if (!isOnce)
            {
                //- �U���̐ݒ�
                vibration.SetVibration(60, 1.0f);
                // ��������
                isOnce = true;

                // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
                // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
                // �w�肵���ʒu�ɐ���
                GameObject fire = Instantiate(
                    particleObject,                     // ����(�R�s�[)����Ώ�
                    transform.position,           // ���������ʒu
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                    );
                //----- �e�����̂܂ܓ����蔻��ɗ��p����
                // �������ɕ`�����߂�
                GetComponent<MeshRenderer>().enabled = false;
                gameObject.tag = "ExplodeCollision";
                rb.isKinematic = true;
                GetComponent<DestroyTimer>().enabled = true;
                GetComponent<SphereCollider>().isTrigger = true;
                //- ���̍Đ�
                gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            }
        }
    }
}
