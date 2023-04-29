using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}