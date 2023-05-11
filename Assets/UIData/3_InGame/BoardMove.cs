/*
 ===================
 ����F���
 �M�~�b�N�K�C�h�A�j���[�V�������Ǘ�����X�N���v�g
 ===================
 */

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Video;
#if UNITY_EDITOR
//- �f�v���C����Editor�X�N���v�g������ƃG���[�BUNITY_EDITOR�Ŋ���
using UnityEditor;
#endif

//- �M�~�b�N�����Ŕ̃A�j���[�V����
public class BoardMove : MonoBehaviour
{
    private enum E_OUTDIRECTION
    {
        [Header("��")]
        LEFT,
        [Header("�E")]
        RIGHT,
        [Header("��")]
        UP,
        [Header("��")]
        DOWN,
    }

    private const float LEFT = -2500.0f;
    private const float RIGHT = 3500.0f;
    private const float TOP = 1200.0f;
    private const float DOWN = -1200.0f;

    [SerializeField] private Image img;
    [SerializeField] private VideoPlayer movie;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float DeleyTime = 0.0f;
    private Dictionary<string, Dictionary<string, Vector3>> InitValues;
    public static bool MoveComplete = false;
    //private PController pCnt;
    private void Awake()
    {
        //- �����l�o�^
        InitValues = new Dictionary<string, Dictionary<string, Vector3>>
        {{"����",new Dictionary<string, Vector3>{{"�ʒu",movie.transform.position},}}};
        InitValues.Add("����", new Dictionary<string, Vector3> { { "�ʒu", tmp.transform.position } });
        InitValues.Add("�w�i", new Dictionary<string, Vector3> { { "�ʒu", img.transform.position } });
        //- �����ʒu�X�V
        img.transform.localPosition = new Vector3(LEFT, img.transform.localPosition.y);
        movie.transform.localPosition = new Vector3(LEFT, movie.transform.localPosition.y);
        tmp.transform.localPosition = new Vector3(LEFT, tmp.transform.localPosition.y);
        //- �����~
        movie.Pause();
    }

    /// <summary>
    /// �o�ꋓ�����s��
    /// </summary>
    public void StartMove()
    {
        //- ������^��
        DOTween.Sequence()
            .AppendInterval(1.0f)
            .Append(img.transform.DOMove(InitValues["�w�i"]["�ʒu"], 0.5f))
            .Join(movie.transform.DOMove(InitValues["����"]["�ʒu"], 0.5f))
            .Join(tmp.transform.DOMove(InitValues["����"]["�ʒu"], 0.5f))
            .OnPlay(() => { movie.Play(); })
            .AppendInterval(DeleyTime)
            .OnComplete(() => { OutMove(); });
    }

    /// <summary>
    /// �P�ދ������s��
    /// </summary>
    public void OutMove()
    {
        DOTween.Sequence()
            .Append(movie.transform.DOMoveX(RIGHT, 0.3f))
            .Join(img.transform.DOMoveX(RIGHT, 0.3f))
            .Join(tmp.transform.DOMoveX(RIGHT, 0.3f))
            .OnComplete(() =>
            {
                MoveComplete = true;
                Destroy(gameObject);
            });
        //- �v���C���[�𑀍�\�ɕύX
        GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);
    }

    public static bool GetFirstDraw()
    {
        Debug.Log("Get" + MoveComplete);
        return MoveComplete; }

    public static void SetFirstDraw(bool flag)
    {
        MoveComplete = flag;
        Debug.Log("Set" + MoveComplete);
    }
}

