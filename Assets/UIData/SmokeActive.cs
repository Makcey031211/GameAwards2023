using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeActive : MonoBehaviour
{
    [SerializeField, Header("âå")]
    private List<GameObject> Smokes;
    [SerializeField, Header("ÉvÉåÉCÉÑÅ[â‘âŒÇÃë—")]
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
