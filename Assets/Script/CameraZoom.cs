using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraZoom : MonoBehaviour
{
    GameObject maincamera;
    Transform cameratrans;
    [SerializeField,Header("里ボタン")]
    public GameObject selectpoint;
    RectTransform selecttrans;
    Camera camerasize;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        cameratrans = maincamera.GetComponent<Transform>();
        selecttrans = selectpoint.GetComponent<RectTransform>();
        camerasize = maincamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void OnClick()
    {
        cameratrans.DOMove(
            selecttrans.position, // 移動終了地点
            1f                    // 演出時間
        );
        camerasize.DOOrthoSize(2.0f, 1.0f);
    }
}
