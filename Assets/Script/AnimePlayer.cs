using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

/*
 *  §ìFûü´
 *  TvF¢àÌvC[®
 */
public class AnimePlayer : MonoBehaviour
{
    [SerializeField, Header("XP[ÏXIuWFNg")]
    private GameObject scaleObj;
    [SerializeField, Header("xÔ(b)")]
    private float delayTime;
    [SerializeField, Header("kñÅ¢­Ô(b)")]
    private float shrinkTime;

    public void SetAnime()
    {
        //=== vC[ðXÉkßÄ¢­Aj[V ===
        scaleObj.transform.DOScale(Vector3.zero, shrinkTime)
        .SetDelay(delayTime)
        .OnComplete(() =>
        {
            //- k¬Aj[VªI¹µ½çIuWFNgðñ\¦É·é
            scaleObj.SetActive(false);
        });
    }

    //private IEnumerator ScalePlayer(float delayTime)
    //{
    //    //- ScalePlayer\bhðdelayTimebãÉÄÑo·
    //    //StartCoroutine(ScalePlayer(delayTime));

    //    ////- delayTimebÒ@·é
    //    //yield return new WaitForSeconds(delayTime);

    //    ////- Aj[VÌJnðÝè
    //    //float startTime      = Time.time;
    //    ////-  ÌúXP[ðÝè
    //    //Vector3 initialScale = transform.localScale;

    //    ////=== vC[ðXÉkßÄ¢­Aj[V ===
    //    ////- wèµ½bªAAj[V³¹é
    //    //while (Time.time < startTime + shrinkTime)
    //    //{
    //    //    //- Aj[VÌisxðvZ
    //    //    float t = (Time.time - startTime) / shrinkTime;
    //    //    //- XP[ðXÉÏ»³¹é
    //    //    transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
    //    //    //- Ìt[ÜÅÒ@
    //    //    yield return null;
    //    //}
    //}
}