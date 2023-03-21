using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")]
    private SceneObject NextScene;
    void Start()
    {   
    }
    
    public void MoveScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}
