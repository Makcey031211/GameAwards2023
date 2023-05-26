using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseEncrypt : MonoBehaviour
{
    [SerializeField, Header("ˆÃ†‰»ƒtƒ‰ƒO")]
    private bool isEncrypt;

    private void OnValidate()
    {
        SaveManager save = GetComponent<SaveManager>();
        save.SetEncryptFlag(isEncrypt);
        //Debug.Log(isEncrypt);
    }
}
