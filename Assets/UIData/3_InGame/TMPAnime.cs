using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TMPAnime : MonoBehaviour
{
    private enum E_TEXTCOLOR
    {
        Clear,  //���F����
        Black,  //��
        Blue,   //��
        Cyan,   //�V�A��
        Gray,   //�D�F
        Green,  //��
        Magenta,//�}�[���^
        Red,    //��
        White,  //��
        Yellow  //���F
    }

    [SerializeField, Header("�A�j���[�V����������e�L�X�g")]
    private TextMeshProUGUI TMP;
    [SerializeField, Header("�e�L�X�g�����J���[")]
    private E_TEXTCOLOR textcolor = E_TEXTCOLOR.Black;
    [SerializeField, Header("�e�L�X�g�A�j���J���[")]
    private E_TEXTCOLOR textAnicolor = E_TEXTCOLOR.Black;
    [SerializeField, Header("��]�b")]
    private float RotateTime = 0.0f;
    [SerializeField, Header("�g�̍���")]
    private float WaveTop = 0.0f;
    [SerializeField, Header("�g�ړ������܂ł̎���")]
    private float WaveTime = 0.0f;
//    [SerializeField, Header("�C�[�W���O�ݒ�")]
    private float EaseTime = 2.0f;
    [SerializeField, Header("�����t�F�[�h�����܂ł̎���")]
    private float FadeTime = 0.0f;
    [SerializeField, Header("�J���[�x������")]
    private float DelayColor = 0.0f;
    [SerializeField, Header("���[�v�x������")]
    private float DelayLoop = 0.0f;

    //- �N���A���̍Đ���������Ă��邩
    private bool bPermissionClearSE = false;

    private Vector3 initialScale;
    private void Awake()
    {
        TMP.color = GetColor(textcolor);
        TMP.DOFade(0f, 0f);
        DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(TMP);
        //- ���߂̃e�L�X�g��90�x��]�����Ă���
        for(int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
        { tmpAnimator.DORotateChar(i, Vector3.up * 90, 0); }
    }
   

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        StartCoroutine(AnimationCoroutine());
    }

    private void OnDisable()
    {     SceneManager.sceneUnloaded -= OnSceneUnloaded;    }

    private void OnSceneUnloaded(Scene scene)
    {    DOTween.KillAll(); }

    IEnumerator AnimationCoroutine()
    {
        //- �N���A���Đ�
        if (bPermissionClearSE)
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Clear);
        else
            bPermissionClearSE = true;
        DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(TMP);
        while (tmpAnimator.textInfo.characterCount == 0)
        {
            yield return null;
        }
        for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
        {
            FirstAnime(tmpAnimator, i);
        }
        while (true)
        {
            yield return new WaitForSeconds(DelayLoop);
            for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
            {   LoopAnime(tmpAnimator, i);  }
        }

    }

    private void FirstAnime(DOTweenTMPAnimator tmpAnimator, int i)
    { 
        //- ���߂̃e�L�X�g��90�x��]�����Ă���
        tmpAnimator.DORotateChar(i, Vector3.up * 90, 0);
        //- �w�肳�ꂽ�����ɑ΂��ăA�j���[�V������ݒ肷��
        Vector3 currCharOffset = tmpAnimator.GetCharOffset(i);
        DOTween.Sequence()
            .Append(tmpAnimator //���ʒu�ɉ�]������
                .DORotateChar(i, Vector3.zero, RotateTime))
            .Append(tmpAnimator //�ړ�
                .DOOffsetChar(i, currCharOffset + new Vector3(0, WaveTop, 0), WaveTime).SetEase(Ease.OutFlash, EaseTime))
            .Join(tmpAnimator   //�������t�F�[�h������
                .DOFadeChar(i, 1, FadeTime))
            .AppendInterval(DelayColor)  //�x��
            .Append(tmpAnimator     //�w�肳�ꂽ�J���[���悹��̂�2��J��Ԃ�
                .DOColorChar(i, GetColor(textAnicolor), 0.2f).SetLoops(2, LoopType.Yoyo))
            .SetDelay(0.07f * i) //�x��
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)    
            .SetLink(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tmpAnimator"></param>
    /// <param name="i"></param>
    private void LoopAnime(DOTweenTMPAnimator tmpAnimator, int i)
    {
        //- �w�肳�ꂽ�����ɑ΂��ăA�j���[�V������ݒ肷��
        Vector3 currCharOffset = tmpAnimator.GetCharOffset(i);
        DOTween.Sequence()
            .Append(tmpAnimator //�ړ�
                .DOOffsetChar(i, currCharOffset + new Vector3(0, WaveTop, 0), WaveTime).SetEase(Ease.OutFlash, EaseTime))
            .Join(tmpAnimator   //�������t�F�[�h������
                .DOFadeChar(i, 1, FadeTime))
            .AppendInterval(DelayColor)  //�x��
            .Append(tmpAnimator     //�w�肳�ꂽ�J���[���悹��̂�2��J��Ԃ�
                .DOColorChar(i, GetColor(textAnicolor), 0.2f).SetLoops(2, LoopType.Yoyo))
            .SetDelay(0.07f * i) //�x��
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .SetLink(gameObject);
    }

    /// <summary>
    /// �e�L�X�g�̐F���擾����
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private Color GetColor(E_TEXTCOLOR color)
    {
        switch (color)
        {
            case E_TEXTCOLOR.Clear:
                return new Color(1f, 1f, 1f, 0f);
            case E_TEXTCOLOR.Black:
                return Color.black;
            case E_TEXTCOLOR.Blue:
                return Color.blue;
            case E_TEXTCOLOR.Cyan:
                return Color.cyan;
            case E_TEXTCOLOR.Gray:
                return Color.gray;
            case E_TEXTCOLOR.Green:
                return Color.green;
            case E_TEXTCOLOR.Magenta:
                return Color.magenta;
            case E_TEXTCOLOR.Red:
                return Color.red;
            case E_TEXTCOLOR.White:
                return Color.white;
            case E_TEXTCOLOR.Yellow:
                return Color.yellow;
            default:
                return Color.black;
        }
    }


}
