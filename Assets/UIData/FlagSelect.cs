using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class FlagSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField, Header("ステージ番号")]
    private int StageNum = -1;
    private Button btn;
    private SaveManager save;
    private void Awake()
    {
        save = new SaveManager();
        btn = GetComponent<Button>();
    }
    void Update()
    {
        bool clear = save.GetStageClear(StageNum);
        if (StageNum > 0 && clear)
        {
     
            btn.interactable = true;
        }
        else
        {   btn.interactable = false;   }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(!btn.interactable && eventData.selectedObject != gameObject)
        { EventSystem.current.SetSelectedGameObject(null); }
    }

    public void OnDeselect(BaseEventData eventData)
    {
    }
}
