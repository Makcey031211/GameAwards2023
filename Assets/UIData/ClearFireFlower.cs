using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearFireFlower : MonoBehaviour
{
    [SerializeField, Header("ç≈ëÂÉTÉCÉYÇ…Ç»ÇÈéûä‘")]
    private float SizeTime;

    private Image img;
    private Vector2 Size;
    void Start()
    {
        img = GetComponent<Image>();
        Size = img.transform.localScale;
        img.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        transform.DOScale(new Vector3(Size.x, Size.y), SizeTime)
            .SetLink(img.gameObject,LinkBehaviour.PauseOnDisablePlayOnEnable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
