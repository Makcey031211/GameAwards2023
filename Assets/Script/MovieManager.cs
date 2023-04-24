using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MovieManager : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h�̒���(�b)")]
    private float FadeTime;

    [SerializeField, Header("�ԉΔ����̒x������(�b)")]
    private float DelayFireflowerTime;

    [SerializeField, Header("�ԉ΂̌�A�t�F�[�h���n�܂�܂ł̎���(�b)")]
    private float DelayFadeTime;

    [SerializeField, Header("�X�e�[�W�`��I�u�W�F�N�g")]
    private GameObject StageDrawObj;


    private SceneChange sceneChange; // �R���g���[���[�̐U���p
    private bool bPlayMovie = false; // ���o�����ǂ���
    private ObjectFade fade; // �t�F�[�h�p�̃X�v���C�g

    void Start()
    {
        //- ���C���J��������V�[���ύX�X�N���v�g�擾
        sceneChange = GameObject.Find("Main Camera").GetComponent<SceneChange>();
        //- �t�F�[�h�p�X�N���v�g�̎擾
        fade = GameObject.Find("FadeImage").GetComponent<ObjectFade>();
    }
    
    void Update()
    {
        if (bPlayMovie) sceneChange.ResetCurrentTime(); //- ���o���A�N���A���o���o�Ȃ��悤�ɂ���
    }

    //- ���o�t���O��ύX����֐�
    public void SetMovieFlag(bool bFlag)
    {
        bPlayMovie = bFlag;
    }

    //- ���o�������J�n����֐�
    public void StartVillageMovie()
    {
        StartCoroutine(MovieSequence());
    }
    private IEnumerator MovieSequence()
    {
        bPlayMovie = true; //- ���o�t���O�ύX

        //- �t�F�[�h��o�ꂳ����
        fade.SetFade(TweenColorFade.FadeState.In, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        //- ���o�V�[����ǉ����[�h,�t�F�[�h��ޏꂳ����
        LoadMovieScene();
        fade.SetFade(TweenColorFade.FadeState.Out, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        //- ��莞�Ԍ�A�ԉ΂𔭐�������
        yield return new WaitForSeconds(DelayFireflowerTime);
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Explosion, 1.0f, false);
        SetActiveFireflower(0, true);

        //- ��莞�Ԍ�A�t�F�[�h��o�ꂳ����
        yield return new WaitForSeconds(DelayFadeTime);
        fade.SetFade(TweenColorFade.FadeState.In, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        //- ���o�V�[�����A�����[�h
        UnloadMovieScene();
        fade.SetFade(TweenColorFade.FadeState.Out, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        bPlayMovie = false; //- ���o�t���O�ύX
    }

    //- ���o�p�V�[���̃��[�h���s���֐�
    private void LoadMovieScene()
    {
        StageDrawObj.SetActive(false); //- �X�e�[�W�I�u�W�F�N�g�̕`�����߂�
        SceneManager.LoadScene("MovieVillage", LoadSceneMode.Additive); //- ���o�p�V�[����ǉ����[�h   
    }

    //- ����̃I�u�W�F�N�g�̃t���O��ύX����֐�
    private void SetActiveFireflower(int childNum, bool bFlag)
    {
        GameObject obj = GameObject.Find("MovieObject"); //- �I�u�W�F�N�g�̌���
        obj.transform.GetChild(childNum).gameObject.SetActive(bFlag); //- �t���O�ύX
    }

    //- ���o�p�V�[���̃A�����[�h���s���֐�
    private void UnloadMovieScene()
    {
        StageDrawObj.SetActive(true); //- �X�e�[�W�I�u�W�F�N�g�̕`����ĊJ
        SceneManager.UnloadScene("MovieVillage"); //- ���o�p�V�[���̃A�����[�h
    }
}
