using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CutIn : MonoBehaviour
{
    [SerializeField] private Image BossImg;
    private TextMeshProUGUI tmp;

    private Vector3 InitSize;
    private void Awake()
    {
        InitSize = BossImg.transform.localScale;
        BossImg.rectTransform.localScale = Vector3.zero;
    }
    void Start()
    {
        BossImg.transform.DOScale(InitSize, 1.0f).OnComplete(()=> {
            BossImg.transform.DOMove(new Vector3(-500, 0, 0), 0.75f).SetRelative(true);
        });

    }

    
    void Update()
    {
        
    }
}
