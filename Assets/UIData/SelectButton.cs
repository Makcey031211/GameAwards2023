using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    //private enum E_SelectScene
    //{
    //    Next,
    //    Retry
    //}

    [SerializeField, Header("ÉVÅ[ÉìëJà⁄êÊ")]
    private SceneObject NextScene;

    private SceneChange scenechange;
  //  private SceneObject NextScene;
    void Start()
    {   
        //NextScene = scenechange.GetNextScene();
        Debug.Log(NextScene);
    }
    
    public void MoveScene()
    {
        SceneManager.LoadScene(NextScene);

        //switch (SelectScene)
        //{
        //    case E_SelectScene.Next:
        //        SceneManager.LoadScene(NextScene);
        //        break;
        //    case E_SelectScene.Retry:
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //        break;
        //}
    }
}
