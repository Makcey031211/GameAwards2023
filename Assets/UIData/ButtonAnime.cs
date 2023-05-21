/*
 ===================
 ����F���
 �{�^���A�j���[�V�������Ǘ�����X�N���v�g
 ===================
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;      
#endif

//- �{�^���A�j���[�V�����N���X
public class ButtonAnime : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    ISubmitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Color OverTextColor;
    private Color BaseTextColor;

    public bool bPermissionSelectSE = true; // �I��SE�̍Đ���������Ă��邩

    private Button button;
    private Vector2 BaseSize;
    private Tween currentTween;
    private bool bSelect = false;

    void Awake()
    {
        if (image == null)
        { return; }
        button = GetComponent<Button>();
        image.fillAmount = 0;
        BaseTextColor = tmp.color;
    }

    //- �I�������ۂ̏���
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if (image == null)
        { return; }
        image.DOFillAmount(1.0f, 0.25f).SetEase(Ease.OutCubic).Play();
        tmp.DOColor(OverTextColor, 0.25f).Play();
        //- �I�����Đ�
        if (bPermissionSelectSE)
            SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Select);
        else
            bPermissionSelectSE = true;
    }

    /// <summary>
    /// �I�����O�ꂽ�ۂ̏���
    /// </summary>
    /// <param name="eventData"></param>
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        if (image == null)
        { return; }

        //- �A�ő΍�t���O�������Ă��Ȃ���Ώ���
        if (!bSelect)
        {
            Debug.Log("a");
            image.DOFillAmount(0.0f, 0.25f).SetEase(Ease.OutCubic).Play();
            tmp.DOColor(BaseTextColor, 0.25f).Play();
        }
    }

    /// <summary>
    /// �{�^���������ꂽ�ۂɍs������
    /// </summary>
    /// <param name="eventData"></param>
    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        //- �I�����Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        image.fillAmount = 1.0f;
    }

    public void PushButtonAnime()
    {
        image.DOColor(new Color(1.0f,0.5f,0.5f), 0.25f);
        image.DOFillAmount(1.0f, 0.0f);
    }

    public void SetbSelect(bool flag)
    {   bSelect = flag; }

    /*�@���[�[�[�[�[�[�g���R�[�h�[�[�[�[�[�[���@*/
#if UNITY_EDITOR
    //- Inspector�g���N���X
    [CustomEditor(typeof(ButtonAnime))] //�K�{
    public class ButtonAnimeEditor : Editor //Editor�̌p��
    {
        public override void OnInspectorGUI()
        {
            ButtonAnime btnAnm = target as ButtonAnime;
            EditorGUI.BeginChangeCheck();
            btnAnm.image 
                = (Image)EditorGUILayout.ObjectField("���삷��摜",btnAnm.image,typeof(Image),true);
            btnAnm.tmp
                = (TextMeshProUGUI)EditorGUILayout.ObjectField("�e�L�X�g", btnAnm.tmp, typeof(TextMeshProUGUI), true);
            btnAnm.OverTextColor
                = EditorGUILayout.ColorField("�J���[", btnAnm.OverTextColor);
            
            //- �C���X�y�N�^�[�̍X�V
            if(GUI.changed)
            {   EditorUtility.SetDirty(target); }
        }
    }
#endif

}
