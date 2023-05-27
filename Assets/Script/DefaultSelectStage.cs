using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultSelectStage : MonoBehaviour
{
    private enum SelectType
    {
        Sato,
        Stage
    }

    [SerializeField]
    private SelectType type;
    [SerializeField]
    private Button[] stageButton;
    [SerializeField]
    private SaveManager saveManager;
    [SerializeField, Header("type‚ªStage‚ÌŽž‚Ì‚Ý—˜—p")]
    private int stageNum;
    [SerializeField]
    private GameObject player;
    int maxClearVillege = 0;

    // Start is called before the first frame update
    void Start()
    {
        int satoValue = 1;
        int stageValue = 0;
        if (type == SelectType.Sato) satoValue *= 10;
        if (type == SelectType.Stage) stageValue = (stageNum - 1) * 10;

        for(int i = 0; i < stageButton.Length - 1; i++) {
            if (saveManager.GetStageClear((i + 1) * satoValue + stageValue)) maxClearVillege++;
        }

        if (type == SelectType.Stage && saveManager.GetStageClear(10 * stageNum)) {
            maxClearVillege = 0;
        }

        stageButton[maxClearVillege].Select();

        if (type == SelectType.Sato || type == SelectType.Stage) {
            player.transform.position = stageButton[maxClearVillege].transform.position;
        }

        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }
}
