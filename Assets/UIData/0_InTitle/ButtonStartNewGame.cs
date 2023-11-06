using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

/*
 ===================
 ����F����
 �ǋL�F���
 �T�v�F�X�^�[�g�{�^���I�����̏���
 ===================
 */
public class ButtonStartNewGame : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڐ�")] private SceneObject NextScene;
    [SerializeField, Header("�x������(�b)")] private float DelayTime;
    [SerializeField, Header("�t�F�[�h�b��")] private float FadeTime;
    [SerializeField, Header("�|�b�v�A�b�v")] private NewGamePopUp popUp;

    //- �X�N���v�g�p�̕ϐ�
    private BGMManager bgmManager;
    private SaveManager saveManager;
    private ButtonAnime button;
    private bool isClick = true;
    private bool isSound = true;

    void Start()
    {
        button      = GetComponent<ButtonAnime>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>(); 
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    public void StartNewGame()
    {
        if (!isClick && !isSound) return; // false�̎��̓��^�[������

        if (saveManager.GetStageClear(1)) { // �X�e�[�W1���N���A����Ă�����
            // �|�b�v�A�b�v�\��
            popUp.PopUpOpen();
        }
        else {
            // �j���[�Q�[������
            NewGameSetup();
        }
    }

    public void NewGameSetup()
    {
        //- �N���b�N�������t���O��ݒ�
        isClick = false;

        //- �͂��߂����I�����A�Q�[���f�[�^�����Z�b�g
        saveManager.ResetSaveData();

        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        isSound = false;

        DOVirtual.DelayedCall(DelayTime, () => GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        button.PushButtonAnime();
        //- �V�[����ς���O��BGM������
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }
}
