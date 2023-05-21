using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePopUp : MonoBehaviour
{
    private Button StartButton;
    private Button NextButton;
    private Button EndButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton = GameObject.Find("ButtonStartGame").GetComponent<Button>();
        NextButton = GameObject.Find("ButtonNextGame").GetComponent<Button>();
        EndButton = GameObject.Find("ButtonEndGame").GetComponent<Button>();

        gameObject.SetActive(false);
    }

    // �|�b�v�A�b�v��\������
    public void PopUpOpen()
    {
        gameObject.SetActive(true); // �|�b�v�A�b�v�̃I�u�W�F�N�g��L����
        // �C�[�W���O�ݒ�
        gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);

        // �|�b�v�A�b�v���̃{�^���ȊO�𖳌���
        StartButton.enabled = false;
        NextButton.enabled = false;
        EndButton.enabled = false;

        // �{�^���̏����I��
        gameObject.transform.Find("ButtonEnter").GetComponent<Button>().Select();
        return;
    }

    public void PopUpClose()
    {
        // �C�[�W���O�ݒ�
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        gameObject.transform.DOScale(0.0f, 0.5f).SetEase(Ease.InCubic).OnComplete(() => { gameObject.SetActive(false); }); // �I�����I�u�W�F�N�g������

        // �|�b�v�A�b�v���̃{�^���ȊO��L����
        StartButton.enabled = true;
        NextButton.enabled = true;
        EndButton.enabled = true;

        // �j���[�Q�[���{�^���I��
        StartButton.GetComponent<Button>().Select();
        return;
    }
}
