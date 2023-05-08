// =============================================
// 
//
//
//
// =============================================

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryFlip : MonoBehaviour
{
    [SerializeField]
    private GameObject[] flips; // 

    private Image fade;

    private int NowFlipNum = 0;
    private bool isFlip = false;


    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("FadeImage").GetComponent<Image>();
        fade.DOFade(0.0f, 1.0f);

        gameObject.transform.DetachChildren();
        for (int i = flips.Length - 1; i >= 0; i--) {
            flips[i].transform.parent = gameObject.transform;
        }
        Debug.Log(flips.Length);
    }

    // Update is called once per frame
    public void OnClick()
    {
        if (NowFlipNum < flips.Length - 1 && !isFlip) {
            isFlip = true;
            flips[NowFlipNum].GetComponent<RectTransform>().DOMoveX(transform.position.x - 18.0f, 2.0f).SetEase(Ease.InOutCubic).OnComplete(() => { isFlip = false; });
            Debug.Log(NowFlipNum);
            NowFlipNum++;
        }
        else if (!isFlip) {
            fade.DOFade(1.0f, 1.5f).OnComplete(() => { SceneManager.LoadScene("1_Village"); });
        }
    }
}
