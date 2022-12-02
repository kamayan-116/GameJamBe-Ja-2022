using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Tree : S_JD_Interact
{
    public Animator UpTreeAnim;
    public GameObject UpTree;
    public GameObject BottomTree;

    protected override void Interaction()
    {
        UpTreeAnim.SetTrigger("Fall");
    }
}
