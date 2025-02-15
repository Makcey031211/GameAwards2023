using UnityEngine;
using UnityEngine.SceneManagement;

/*
 ===================
 §μFϋό΄
 ΗLFO
 TvFBGMπΗ·ιXNvg
 ===================
 */
public class BGMManager : MonoBehaviour
{
    void Start()
    {
        int numMusicPlayers = FindObjectsOfType<BGMManager>().Length;
        if (numMusicPlayers > 1)
        { Destroy(gameObject); }// IuWFNgπjό·ι
        else
        { DontDestroyOnLoad(gameObject); }// V[JΪ΅ΔΰAIuWFNgπjό΅Θ’
    }

    /// <summary>
    /// BGMπν·ι
    /// </summary>
    public void DestroyBGMManager()
    { Destroy(gameObject); }

    /// <summary>
    /// BGMπνΒ\σΤΙ·ι
    /// </summary>
    public void DestroyPossible()
    {
        //- DontDestroyOnLoadΙπο³Ή½IuWFNgπνΒ\Ι·ι
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}