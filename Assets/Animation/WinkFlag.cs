using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinkFlag : MonoBehaviour
{
    [SerializeField, Header("���b�Ɉ��̊m���ŏu�����邩")]
    private int playChance = 10;

    [SerializeField, Header("�����ďu��������")]
    private bool simFrag;

    Animator animator;
    int rangeMax = 0;
    int cnt = 0;

    static private int simRangeMax = 0;
    static readonly int hashStateWink = Animator.StringToHash("Wink");

    private void Start()
    {
        animator = GetComponent<Animator>();
        simRangeMax = 3 * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (simFrag) {
            cnt++;
            rangeMax = playChance * 60;
            if (cnt >= simRangeMax) {
                animator.Play(hashStateWink);
                cnt = 0;
            }
        }
        else {
            rangeMax = playChance * 60;
            if (Random.Range(0, rangeMax) == 0) {
                animator.Play(hashStateWink);
            }
        }
    }
}
