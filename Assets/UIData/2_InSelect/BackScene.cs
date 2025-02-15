using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

/*
 ===================
 §ìFûü´
 ÇLFqìAåì
 TvFNbNV[JÚ·éXNvg
 ===================
 */
public class BackScene : MonoBehaviour
{
    [SerializeField, Header("IvVìÉ©©éÔ(b)")]
    private float OptionTime = 1.0f;
    [SerializeField, Header("V[Ú®ÌtF[hÔ(b)")]
    private float FadeTime   = 1.0f;
    [SerializeField, Header("V[Ú®ÌBGMØèÖ¦Ô(b)")]
    private float disBGMTime = 1.0f;
    [SerializeField, Header("ßéæÌV[ðÝè")]
    private SceneObject backScene;

    public static bool Input = false;   //üÍ»è

    //- XNvgpÌÏ
    BGMManager bgmManager;
    //- C[WÌR|[lg
    Image imageInGame;

    //- {^ª³êÄ¢é©Ç¤©
    bool bIsPushBack = false;

    //- {^ª³ê½Ô
    float bPushTimeBack = 0;

    //- V[Ú®ªnÜÁ½©Ç¤©ÌtO
    bool bIsStartInGame = false;

    private void Start()
    {
        //- R|[lgÌæ¾
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        Input = false;
    }

    private void Update()
    {
        //- V[Ú®ªnÜÁ½©Ç¤©ÌtOðæ¾
        bool bIsMoveScene = false;

        //- V[Ú®ÌtOXV 
        if (bIsStartInGame) bIsMoveScene = true;

        //@ßé{^@
        //- uV[Ú®{^ª³êÄév©ÂuV[Ú®ªnÜÁÄ¢È¢v
        if (bIsPushBack && !bIsMoveScene)
        {
            if (bIsStartInGame == true) return; // ZbgJntOª½ÁÄ¢êÎ^[
            bIsStartInGame = true; // V[JntOð½Äé
            SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click); // NbN¹Ä¶
            GameObject.Find("ColorFadeImage").GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime); // tF[hJn
            DOVirtual.DelayedCall(disBGMTime, () => bgmManager.DestroyBGMManager());  // V[ðÏ¦éOÉBGMðÁ·
            DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(backScene)); // V[Ì[h(x è)
        }
    }

    /// <summary>
    /// Rg[[ðæ¾·éÖ
    /// </summary>
    /// <param name="context"></param>
    public void OnInBack(InputAction.CallbackContext context)
    {
        //- Ið{^ÌüÍªsíê½çðsíÈ¢
        if (SelectButton.Input)
        { return; }

        //- {^ª³êÄ¢éÔAÏðÝè
        if (context.started)
        {
            bIsPushBack = true;
            Input = true;       // üÍtOÏX
        }
        if (context.canceled) { bIsPushBack = false; }
    }

    /// <summary>
    /// |bvÉ^CgÉßé
    /// </summary>
    public void VillagePoPToTitle()
    {
        if (bIsStartInGame == true) return; // ZbgJntOª½ÁÄ¢êÎ^[
        bIsStartInGame = true; // V[JntOð½Äé
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click); // NbN¹Ä¶
        GameObject.Find("ColorFadeImage").GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime); // tF[hJn
        DOVirtual.DelayedCall(disBGMTime, () => bgmManager.DestroyBGMManager());  // V[ðÏ¦éOÉBGMðÁ·
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(backScene)); // V[Ì[h(x è)
    }
}