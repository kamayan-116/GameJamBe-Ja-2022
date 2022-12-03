using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Bath : S_JD_Interact
{
    private void Start()
    {
        interactionType = "GetBath";
    }
    protected override void Interaction()
    {
        //print("TakeBath");
    }
}
