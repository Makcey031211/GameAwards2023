/*
 ===================
 ��Ր���F���
 �ǋL�ҁF���E�q��E�ҁE�����E���O
 �{�^����I�������ۂɓ��삷��X�N���v�g
 ===================
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

//- �{�^���I�����ɓ��삷��N���X
public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڐ�")] private SceneObject NextScene;          //�V�[���J�ڐ�
    [SerializeField, Header("�t�F�[�h�J�n�x������")] private float DelayTime;         //�t�F�[�h���Ăяo�����܂ł̒x������
    [SerializeField, Header("�t�F�[�h�I�u�W�F�N�g")] private GameObject fadeObject;   //�t�F�[�h�p�I�u�W�F�N�g
    [SerializeField, Header("�t�F�[�h�����܂ł̎���")] private float FadeTime;        //�t�F�[�h��������
    
    private BGMManager bgmManager;
    private Button button;
    private ButtonAnime buttonAnime;
    private SelectMovePlayer SelectPlayer;
    private bool Load = false;                      //���d���[�h�}�~

    void Awake()
    {
        buttonAnime = GetComponent<ButtonAnime>();
        button = GetComponent<Button>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }
    
    /// <summary>
    /// �V�[���J�ڂ��s������
    /// �^�C�g���E���I���E�C���Q�[��
    /// </summary>
    public void MoveScene()
    {
        //- �Ăяo���ςȂ珈�����Ȃ�
        if (Load)
        { return; }

        //- �Ăяo���ςɂ���
        Load = true;
        
        //- �{�^���A�j�������݂����珈��
        if(buttonAnime)
        {
            //- �t���O�ύX���{�^���A�j���ɑ��M����
            buttonAnime.SetbSelect(true);
        }
        
        //- �Ăяo���ꂽ��㉺���E�I���𖳌���
        Navigation NoneNavigation = button.navigation;
        NoneNavigation.selectOnUp = null;
        NoneNavigation.selectOnDown = null;
        NoneNavigation.selectOnLeft = null;
        NoneNavigation.selectOnRight = null;
        button.navigation = NoneNavigation;

        //- �{�^���̓��͂��󂯕t���Ȃ�
        button.interactable = false;

        //- �{�^���A�j�������݂��Ă����珈��
        if(buttonAnime)
        { 
            //- �{�^�����̓A�j���[�V����
            buttonAnime.PushButtonAnime();
        }

        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);

        //- ���o�̕`��t���O�����Z�b�g
        if (CutIn.MoveCompleat) { CutIn.ResetMoveComplete(); }
        if (BoardMove.MoveComplete) { BoardMove.ResetMoveComplete(); }
        if (OpeningAnime.MoveCompleat) { OpeningAnime.ResetMoveComplete(); }
        
        //- �x����̏���
        DOVirtual.DelayedCall(DelayTime, ()=> fadeObject.GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        DOVirtual.DelayedCall (FadeTime, ()=> bgmManager.DestroyBGMManager()).SetDelay(DelayTime); 
        DOVirtual.DelayedCall (FadeTime, ()=> SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }

    /// <summary>
    /// �V�[���J�ڂ��s������
    /// ���I��
    /// </summary>
    public void MoveSelectScene()
    {
        //- �Ăяo���ςȂ�s��Ȃ�
        if (Load)
        { return; }

        //- �Ăяo���ςɂ���
        Load = true;

        //- �Ăяo���ꂽ��㉺���E�I���𖳌���
        Navigation NoneNavigation = button.navigation;
        NoneNavigation.selectOnUp = null;
        NoneNavigation.selectOnDown = null;
        NoneNavigation.selectOnLeft = null;
        NoneNavigation.selectOnRight = null;
        button.navigation = NoneNavigation;

        //- �{�^���̓��͂��󂯕t���Ȃ�
        button.interactable = false;

        //- ���o�̕`��t���O�����Z�b�g
        if (CutIn.MoveCompleat) { CutIn.ResetMoveComplete(); }
        if (BoardMove.MoveComplete) { BoardMove.ResetMoveComplete(); }
        if (OpeningAnime.MoveCompleat) { OpeningAnime.ResetMoveComplete(); }
        
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        
        //- �V�[���J�ڗp�A�j���[�V�������Đ�����
        SelectPlayer = GetComponent<SelectMovePlayer>();
        SelectPlayer.InStageMove();
        
        //- �x����̏���
        DOVirtual.DelayedCall(DelayTime, () => fadeObject.GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);

    }
}