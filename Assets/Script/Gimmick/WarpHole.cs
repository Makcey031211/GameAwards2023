using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpHole : MonoBehaviour
{
    [SerializeField, Header("ワープする位置")]
    private Transform warpPoint;

    private void OnTriggerEnter(Collider other)
    {
        //- ワープホールに接触したら指定した位置にワープする
        if (other.CompareTag("Untagged"))
        {
            other.transform.position = warpPoint.position;
        }
    }
}
