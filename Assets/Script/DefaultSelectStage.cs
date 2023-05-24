using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultSelectStage : MonoBehaviour
{
    [SerializeField]
    private Button[] stageButton;
    [SerializeField]
    private SaveManager saveManager;

    int maxClearVillege = 0;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 1; i <= 3 ; i++) {
            if (saveManager.GetStageClear(i * 10)) maxClearVillege++;
            Debug.Log(maxClearVillege);
        }

        stageButton[maxClearVillege].gameObject.SetActive(true);
        stageButton[maxClearVillege].enabled = true;
        stageButton[maxClearVillege].Select();

        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }
}
