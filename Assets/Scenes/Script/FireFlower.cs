using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    [SerializeField, Header("�Ήԗp�̃I�u�W�F�N�g")]
    private GameObject particleObject;

    [SerializeField, Header("�����܂ł̎���(�b)")]
    private float time = 3.0f;

    Rigidbody rb;

    float currentTime; // ��������̌o�ߎ���

    public bool isExploded = false; // �����������ǂ���
    bool isOnce; // ��������񂾂��s��

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = 0.0f;

        isOnce = false;
    }

    // Update is called once per frame
    void Update() {
        //----- �����܂ł̎��ԃJ�E���g
        currentTime += Time.deltaTime;
        if (currentTime >= time) {
            isExploded = true;
        }

        if (isExploded) { // ����������
            if (!isOnce) { // ��������
                isOnce = true;
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
            }
        }
    }

    //private void FixedUpdate() {
    //    if (isExploded) { // ����������
    //        float collSize = power * 0.0004f;
    //        transform.localScale += new Vector3(collSize, collSize, collSize);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.gameObject.tag == "ExplodeCollision") {
    //        enabled = true;
    //        isExploded = true;
    //    }
    //}

    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject.tag == "ExplodeCollision") {
    //        enabled = true;
    //        isExploded = true;
    //    }
    //}
}
