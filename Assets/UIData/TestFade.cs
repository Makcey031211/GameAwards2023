using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;
public class TestFade : MonoBehaviour
{
    private Image image;

    // �t�F�[�h�C�����鎞�ԁi�b�j
    public float fadeInTime = 1f;

    // �t�F�[�h�A�E�g���鎞�ԁi�b�j
    public float fadeOutTime = 1f;

    void Start()
    {
        // Image�R���|�[�l���g���擾����
        image = GetComponent<Image>();

        // �C���[�W�̃A���t�@�l��0�ɏ�����
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        // Dotween���g�p���ăt�F�[�h�C������
        image.DOFade(1f, fadeInTime).SetLink(image.gameObject,LinkBehaviour.PauseOnDisablePlayOnEnable);
    }

    public void FadeOut()
    {
        // Dotween���g�p���ăt�F�[�h�A�E�g����
        image.DOFade(0f, fadeOutTime);
    }


    //[SerializeField]
    //UnityEngine.UI.Image image;

    //void Start()
    //{
    //    //1�b��Image�̃A���t�@��0�ɂ���
    //    //this.image.DOFade(endValue: 0f, duration: 10f);
    //    CanvasGroup c = GetComponent<CanvasGroup>();
    //    c.FadeOut(10f);
        
    //    //image.DOFade(0, 5f);
        
    //}
}
