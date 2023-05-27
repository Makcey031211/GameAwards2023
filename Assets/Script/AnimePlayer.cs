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

    private IEnumerator ScalePlayer(float deltaTime)
    {
        //- delayTimebÒ@·é
        yield return new WaitForSeconds(delayTime);

        float startTime      = Time.time; // JnÔÌXV
        Vector3 initialScale = transform.localScale; // å«³ÌÏ

        //- vC[ðXÉkßÄ¢­Aj[V
        while (Time.time < startTime + shrinkTime)
        {
            float t = (Time.time - startTime) / shrinkTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
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