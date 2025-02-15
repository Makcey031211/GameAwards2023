using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

/*
 ===================
 §μFϋό΄
 ΗLFδγ
 TvFX^[g{^IπΜ
 ===================
 */
public class ButtonStartNewGame : MonoBehaviour
{
    [SerializeField, Header("V[JΪζ")] private SceneObject NextScene;
    [SerializeField, Header("xΤ(b)")] private float DelayTime;
    [SerializeField, Header("tF[hb")] private float FadeTime;
    [SerializeField, Header("|bvAbv")] private NewGamePopUp popUp;

    //- XNvgpΜΟ
    private BGMManager bgmManager;
    private SaveManager saveManager;
    private ButtonAnime button;
    private bool isClick = true;
    private bool isSound = true;

    void Start()
    {
        button      = GetComponent<ButtonAnime>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>(); 
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    public void StartNewGame()
    {
        if (!isClick && !isSound) return; // falseΜΝ^[·ι

        if (saveManager.GetStageClear(1)) { // Xe[W1ͺNA³κΔ’½η
            // |bvAbv\¦
            popUp.PopUpOpen();
        }
        else {
            // j[Q[
            NewGameSetup();
        }
    }

    public void NewGameSetup()
    {
        //- NbN³ψ»tOπέθ
        isClick = false;

        //- ΝΆί©ηπIπAQ[f[^πZbg
        saveManager.ResetSaveData();

        //- NbNΉΔΆ
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        isSound = false;

        DOVirtual.DelayedCall(DelayTime, () => GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        button.PushButtonAnime();
        //- V[πΟ¦ιOΙBGMπΑ·
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }
}
