using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulClk_Test : MonoBehaviour
{
    Animator anim;
    public GameObject obj;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SoulClk()
    {
        anim.SetBool("IsShow", true);
        obj.SetActive(true);
    }
}
