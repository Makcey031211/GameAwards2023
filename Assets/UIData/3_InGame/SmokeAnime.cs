using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SmokeAnime : MonoBehaviour
{
    [SerializeField, Header("����Ɋ|���鎞�Ԃ̐U�ꕝ������l")]
    private int RandomRoteUPTime = 0;
    [SerializeField, Header("����Ɋ|���鎞�Ԃ̐U�ꕝ�������l")]
    private int RandomRoteDOWNTime = 0;
    [SerializeField, Header("�v���C���[�o���܂ł̒x������")]
    private float DelayTime = 0;
    [SerializeField, Header("�t�F�[�h�����^�C��")]
    private float FadeTime = 0;

    private Image img;
    private Vector2 InitSise;
    private Vector2 InitPos;
    private float RoteTime;
    private bool SmokeMove = false;
    private void Awake()
    {
        img = GetComponent<Image>();
        RoteTime = Random.Range((float)RandomRoteDOWNTime, (float)RandomRoteUPTime);
        //- �����T�C�Y��ۑ�
        InitSise = img.transform.localScale;
        //- �T�C�Y��0�ɂ���
        img.transform.localScale = new Vector3(0, 0, 0);
        
    }

    void Start()
    {
        ////- �ړ�����
        //transform
        //    .DOMove(new Vector3(InitPos.x,InitPos.y, 0), RoteTime)
        //    .SetEase(Ease.Linear)
        //    .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
        //- �g�又��
        transform.DOScale(InitSise, 0.5f);

        //- ��]����
        transform
            .DORotate(new Vector3(0, 0, 360.0f), RoteTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1)   //�i�����[�v
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
        //- �x��
        DOTween.Sequence()
            .SetDelay(DelayTime)
            .OnComplete(() => 
            { SmokeMove = true; });

        //- �t�F�[�h
        img.DOFade(0, FadeTime)
            .SetEase(Ease.OutSine)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .OnComplete(()=> 
            {
                Destroy(gameObject);
            });
    }

    public bool GetSmokeMove()
    {   return SmokeMove;    }
}
