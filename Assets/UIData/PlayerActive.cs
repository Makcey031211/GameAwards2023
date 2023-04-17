using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActive : MonoBehaviour
{
    [SerializeField, Header("プレイヤー")]
    private GameObject player;
    [SerializeField, Header("リセットゲージ")]
    private GameObject Reset;
    [SerializeField,Header("プレイヤー花火の帯")]
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
