using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeActive : MonoBehaviour
{
    [SerializeField, Header("��")]
    private List<GameObject> Smokes;
    [SerializeField, Header("�v���C���[�ԉ΂̑�")]
    private FireBelt belt;

    private bool Acitve = false;
    private void Awake()
    {
        foreach(GameObject o in Smokes)
        {   o.SetActive(false); }   
    }

    void Update()
    {
        if (!Acitve && belt.GetMoveComplete())
        {
            foreach (GameObject o in Smokes)
            {   o.SetActive(true);  }
            Acitve = true;
        }
    }

    public bool GetSmokeActive()
    {
        return Acitve;
    }
}
