using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerKunDelete : MonoBehaviour
{
    
    private FireBelt Belt;
    private void Awake()
    {
        Belt = transform.parent.GetComponent<FireBelt>();
    }
    void Update()
    {
        if(Belt.GetDeleteFlag())
        {
            //- 自分自身を削除する
            Destroy(gameObject);
        }
    }
}
