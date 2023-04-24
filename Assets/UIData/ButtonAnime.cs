using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

#if UNITY_EDITOR
//- �f�v���C����Editor�X�N���v�g������ƃG���[�BUNITY_EDITOR�Ŋ���
using UnityEditor;      
#endif

//- �{�^���A�j���[�V�����N���X
public class ButtonAnime : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    ISubmitHandler
{
    //- �A�j���[�V�������̃p�^�[��
    private enum E_ANIMATIONTYPE
    {
        [InspectorName("�g�k����")]
        PopMove,
        [InspectorName("�t�F�[�h����")]
        Fade,
        [InspectorName("�A�j���[�V�������s��Ȃ�")]
        None
    };

    [SerializeField] private E_ANIMATIONTYPE animetype = E_ANIMATIONTYPE.PopMove;   //�����^�C�v 
    [SerializeField] private Vector2 SelectSize = new Vector2(1.1f,1.1f);        //�|�b�v
    [SerializeField] private float AlphaNum = 0.0f;  //Fade
    [SerializeField] private float MoveTime = 0.1f;  //���슮������
    [SerializeField] private bool Loop = false;      //���[�v���邩
    
    public bool bPermissionSelectSE = true; // �I��SE�̍Đ���������Ă��邩

    private Button button;
    private Vector2 BaseSize;
    private Tween currentTween;

    void Awake()
    {
        button = GetComponent<Button>();
        //- �^�C�v���|�b�v�ł���Ώ����T�C�Y�ۑ�
        if(animetype == E_ANIMATIONTYPE.PopMove)
        {   BaseSize = button.transform.localScale; }
    }

    //- �I�������ۂ̏���
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        //- �A�j���[�V���������삵�Ă�����A�j���[�V�������폜����
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        {   currentTween.Kill();    }
        //- �^�C�v���Ƃɕʏ������s��
        switch (animetype)
        {
            //- �|�b�v����
            case E_ANIMATIONTYPE.PopMove:
                if(Loop)
                {
                    transform.DOScale(
                        new Vector3(BaseSize.x * SelectSize.x, BaseSize.y * SelectSize.y), MoveTime)
                        .SetEase(Ease.OutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                }
                else
                {
                    transform.DOScale(
                        new Vector3(BaseSize.x * SelectSize.x, BaseSize.y * SelectSize.y), MoveTime)
                        .SetEase(Ease.OutSine);
                }
                break;
           //- �t�F�[�h����
            case E_ANIMATIONTYPE.Fade:
                if(Loop)
                {
                    button.image.DOFade(AlphaNum, MoveTime)
                       .SetEase(Ease.OutSine)
                       .SetLoops(-1, LoopType.Yoyo);
                }
                else
                {
                    button.image.DOFade(AlphaNum, MoveTime)
                       .SetEase(Ease.OutSine);
                }
                break;
            //- �����Ȃ�
            case E_ANIMATIONTYPE.None:
                break;
        }
        //- �I�����Đ�
        if (bPermissionSelectSE)
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Select, 1.0f, false);
        else
            bPermissionSelectSE = true;
    }

    //- �I���������̏���
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        //- �A�j���[�V�������c���Ă�����A�j���[�V�������폜
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        {
            currentTween.OnComplete(() =>
            {
                //- ���ڂ��Ƃɏ������s��
                switch (animetype)
                {
                    //- �|�b�v����
                    case E_ANIMATIONTYPE.PopMove:
                        transform.DOKill();
                        transform.localScale = BaseSize;
                        break;
                    //- �t�F�[�h����
                    case E_ANIMATIONTYPE.Fade:
                        button.image.DOKill();
                        button.image.DOFade(1.0f, 0.0f);
                        break;
                    //- �����Ȃ�
                    case E_ANIMATIONTYPE.None:
                        break;
                }
            });
            currentTween.Kill();
        }
        else
        {
            //- ���ڂ��Ƃɏ������s��
            switch (animetype)
            {
                //- �|�b�v����
                case E_ANIMATIONTYPE.PopMove:
                    transform.DOKill();
                    transform.localScale = BaseSize;
                    break;
                //- �t�F�[�h����
                case E_ANIMATIONTYPE.Fade:
                    button.image.DOKill();
                    button.image.DOFade(1.0f, 0.0f);
                    break;
                //- �����Ȃ�
                case E_ANIMATIONTYPE.None:
                    break;
            }
        }
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        Debug.Log("o");
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        { currentTween.Kill(); }
        var submit = DOTween.Sequence();
        //submit.Append(transform.DOScale(new Vector3(BaseSize.x, BaseSize.y), MoveTime))
        //    .OnComplete(() =>
        //    {
                //- �I�����Đ�
                SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click, 1.0f, false);
            //});
        currentTween = submit;
    }


    /*�@���[�[�[�[�[�[�g���R�[�h�[�[�[�[�[�[���@*/
#if UNITY_EDITOR
    //- Inspector�g���N���X
    [CustomEditor(typeof(ButtonAnime))] //�K�{
    public class ButtonAnimeEditor : Editor //Editor�̌p��
    {
        bool folding = false; //�܂��݃t���O

        public override void OnInspectorGUI()
        {
            ButtonAnime btnAnm = target as ButtonAnime;

            /*�@���[�[�[�J�X�^���\���[�[�[���@*/
            //- �񋓌^�ɍ��킹�ĕ\����ύX
            EditorGUI.BeginChangeCheck();
            btnAnm.animetype = (ButtonAnime.E_ANIMATIONTYPE)EditorGUILayout.EnumPopup("�A�j���[�V�����̎��",btnAnm.animetype);

            //- animetype���ɕ\������ϐ���ύX����
            switch (btnAnm.animetype)
            {
                //- �|�b�v����
                case E_ANIMATIONTYPE.PopMove:
                    
                    folding = 
                        EditorGUILayout.BeginFoldoutHeaderGroup(folding, "�|�b�v�����̐ݒ荀��");
                    
                    if(folding)
                    {
                        //- �g��T�C�Y�̐ݒ荀��
                        btnAnm.SelectSize = 
                            EditorGUILayout.Vector2Field("�I�����̊g��T�C�Y", btnAnm.SelectSize);
                        //- �ړ��������Ԑݒ荀��
                        btnAnm.MoveTime =
                            EditorGUILayout.FloatField("���������܂ł̎���", btnAnm.MoveTime);
                        //- ���[�v�ݒ荀��
                        btnAnm.Loop =
                            EditorGUILayout.Toggle("���[�v�����邩", btnAnm.Loop);
                    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    break;
                //- �t�F�[�h����
                case E_ANIMATIONTYPE.Fade:
                    folding =
                       EditorGUILayout.BeginFoldoutHeaderGroup(folding, "�t�F�[�h�����̐ݒ荀��");

                    if (folding)
                    {
                        //- �A���t�@�l�̐ݒ荀��
                        btnAnm.AlphaNum =
                            EditorGUILayout.FloatField("�ŏ��A���t�@�l", btnAnm.AlphaNum);
                        //- �ړ��������Ԑݒ荀��
                        btnAnm.MoveTime =
                            EditorGUILayout.FloatField("���������܂ł̎���", btnAnm.MoveTime);
                        //- ���[�v�ݒ荀��
                        btnAnm.Loop =
                            EditorGUILayout.Toggle("���[�v�����邩", btnAnm.Loop);
                    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    break;

                //- �A�j���[�V�������s��Ȃ�
                case E_ANIMATIONTYPE.None:
                    folding =
                       EditorGUILayout.BeginFoldoutHeaderGroup(folding, "���ڂȂ�");
                    if(folding)
                    {   Debug.Log("[UI]�{�^���A�j���[�V�������s���Ă��܂���");    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    break;
                
            }
            
            //- �C���X�y�N�^�[�̍X�V
            if(GUI.changed)
            {   EditorUtility.SetDirty(target); }
        }
    }
#endif

}
