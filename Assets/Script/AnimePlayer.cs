using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    //- {^ª³êÄ¢é©Ç¤©
    bool bIsPushAnime = false;

    public void SetAnime()
    {
        //- ScalePlayer\bhðdelayTimebãÉÄÑo·
        StartCoroutine(ScalePlayer(delayTime));
    }

    private IEnumerator ScalePlayer(float delayTime)
    {
        //- delayTimebÒ@·é
        yield return new WaitForSeconds(delayTime);

        //- Aj[VÌJnðÝè
        float startTime      = Time.time;
        //-  ÌúXP[ðÝè
        Vector3 initialScale = transform.localScale;

        //=== vC[ðXÉkßÄ¢­Aj[V ===
        //- wèµ½bªAAj[V³¹é
        while (Time.time < startTime + shrinkTime)
        {
            //- Aj[VÌisxðvZ
            float t = (Time.time - startTime) / shrinkTime;
            //- XP[ðXÉÏ»³¹é
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            //- Ìt[ÜÅÒ@
            yield return null;
        }

        //- k¬Aj[VªI¹µ½çIuWFNgðñ\¦É·é
        scaleObj.SetActive(false);
    }

    /// <summary>
    /// Rg[[æ¾Ö
    /// </summary>
    /// <param name="context"></param>
    public void OnAnime(InputAction.CallbackContext context)
    {
        //- {^ª³êÄ¢éÔAÏðÝè
        if (context.started)  { bIsPushAnime = true; }
        if (context.canceled) { bIsPushAnime = false; }
    }
}