using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActive : MonoBehaviour
{
    [SerializeField, Header("�v���C���[")]
    private GameObject player;
    [SerializeField, Header("���Z�b�g�Q�[�W")]
    private GameObject Reset;
    [SerializeField,Header("�v���C���[�ԉ΂̑�")]
    private SmokeAnime smoke;

    private bool Acitve = false;
    private void Awake()
    {
        player.SetActive(false);
        Reset.SetActive(false);
    }

    void Update()
    {
        if(!Acitve && smoke.GetSmokeMove())
        {
            player.SetActive(true);
            Reset.SetActive(true);
            Acitve = true;
        }
    }
}
