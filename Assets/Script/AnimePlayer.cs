using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //- ScalePlayer\bhðdelayTimebãÉÄÑo·
        StartCoroutine(ScalePlayer(delayTime));
    }

    private IEnumerator ScalePlayer(float deltaTime)
    {
        //- delayTimebÒ@·é
        yield return new WaitForSeconds(delayTime);

        float startTime = Time.time; // JnÔÌXV
        Vector3 initialScale = transform.localScale; // å«³ÌÏ

        //- vC[ðXÉkßÄ¢­Aj[V
        while (Time.time < startTime + shrinkTime)
        {
            float t = (Time.time - startTime) / shrinkTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }
    }
}
