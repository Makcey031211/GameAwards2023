using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    void Start()
    {
        int numMusicPlayers = FindObjectsOfType<BGMManager>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DestroyBGMManager()
    {
        Destroy(gameObject);
    }

    public void DestroyPossible()
    {
        // DontDestroyOnLoad�ɔ������I�u�W�F�N�g���폜�\�ɂ���
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}